using System.Text;
using System.Text.Json;
using RabbitMQ.Client.Events;
using VaultLab.Domain.Entities;
using Microsoft.Extensions.Options;
using VaultLab.Infrastructure.Messaging;
using VaultLab.Application.Abstractions;
using VaultLab.Application.Contracts.Messaging;
using VaultLab.Infrastructure.Persistence;
using RabbitMQ.Client;

namespace VaultLab.Worker.Messaging
{
    public sealed class DocumentProcessingConsumer(
        RabbitMqConnection rabbitMqConnection,
        IOptions<RabbitMqExchangeOptions> exchangeOptions,
        IOptions<RabbitMqQueueOptions> queueOptions,
        IOptions<RabbitMqRetryOptions> retryOptions,
        IServiceScopeFactory scopeFactory,
        ILogger<DocumentProcessingConsumer> logger
        )
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting document processing consumer...");

            var connection = await rabbitMqConnection.GetConnectionAsync(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(
                new CreateChannelOptions(
                    publisherConfirmationsEnabled: true,
                    publisherConfirmationTrackingEnabled: true
                ),
                cancellationToken: cancellationToken
            );


            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, eventArgs) =>
            {

                var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                var documentUploaded = JsonSerializer.Deserialize<DocumentUploadMessage>(message);

                if (documentUploaded is null)
                    return;

                using var scope = scopeFactory.CreateScope();


                var documentRepository =
                    scope.ServiceProvider.GetRequiredService<IDocumentRepository>();

                var textExtractorFactory =
                    scope.ServiceProvider.GetRequiredService<IDocumentTextExtractorFactory>();



                var fileStorage =
                        scope.ServiceProvider.GetRequiredService<IFileStorage>();


                var chunker =
                    scope.ServiceProvider.GetRequiredService<IDocumentChunker>();

                var embeddingGenerator =
                    scope.ServiceProvider.GetRequiredService<IEmbeddingGenerator>();


                logger.LogInformation("Received document: {DocumentId}", documentUploaded.DocumentId);

                var document = await documentRepository.GetByIdAsync(documentUploaded.DocumentId, cancellationToken);


                if (document is null)
                {
                    logger.LogWarning("Document is not found: {DocumentId}", documentUploaded.DocumentId);

                    return;
                }

                //Handle Duplicate Processing
                if (document.Status == Domain.Enums.DocumentStatus.Processed)
                {
                    logger.LogInformation("Document is already processed. Skipping duplicate message: {DocumentId}", document.Id);


                    await channel.BasicAckAsync(
                        eventArgs.DeliveryTag,
                        multiple: false,
                        cancellationToken: cancellationToken
                    );
                    return;
                }

                logger.LogInformation("Processing document: {DocumentId}", document.Id);

                try
                {
                    document.MarkAsProcessing();

                    await documentRepository.SaveChangesAsync(cancellationToken);

                    //Open Record File
                    await using var fileStream = await fileStorage.OpenReadAsync(document.StoragePath, cancellationToken);

                    //get the right extractor 

                    var textExtractor = textExtractorFactory.Create(document.ContentType);

                    //Extract the text;
                    var text = await textExtractor.ExtractAsync(fileStream, document.ContentType, cancellationToken);


                    //Chunk Text
                    var chunkContents = chunker.Chunk(text);

                    var chunks = chunkContents
                        .Select((content, index) =>
                            new DocumentChunk(
                                document.Id,
                                content,
                                index
                            ))
                        .ToList();

                    var embeddings = new List<float[]>();

                    foreach (var chunk in chunks)
                    {
                        var embedding = await embeddingGenerator.GenerateAsync(
                            chunk.Content,
                            cancellationToken
                        );

                        embeddings.Add(embedding.ToArray());
                    }

                    await documentRepository.AddChunkAsync(chunks, embeddings, cancellationToken);


                    document.MarkAsProcessed();
                    await documentRepository.SaveChangesAsync();


                    await channel.BasicAckAsync(
                        eventArgs.DeliveryTag,
                        multiple: false,
                        cancellationToken: cancellationToken
                    );
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Document processing failed: {DocumentId}", document.Id);

                    await HandleFailureAsync(
                        documentId: document.Id,
                        documentRepository: documentRepository,
                        channel: channel,
                        eventArgs: eventArgs,
                        cancellationToken: cancellationToken
                    );
                }
            };

            await channel.BasicConsumeAsync(
                queue: queueOptions.Value.DocumentProcessing,
                autoAck: false,
                consumer: consumer,
                consumerTag: string.Empty,
                noLocal: false,
                exclusive: false,
                arguments: null,
                cancellationToken: cancellationToken
            );

            await Task.Delay(Timeout.Infinite, cancellationToken);
        }

        public async Task HandleFailureAsync(
            IChannel channel,
            BasicDeliverEventArgs eventArgs,
            IDocumentRepository documentRepository,
            Guid documentId,
            CancellationToken cancellationToken
        )
        {
            int retryCount = 0;
            if (eventArgs.BasicProperties.Headers?.TryGetValue(
                    "x-retry-count",
                    out var retryHeader) == true)
            {
                retryCount = retryHeader switch
                {
                    byte[] bytes => int.Parse(
                        Encoding.UTF8.GetString(bytes)),

                    int value => value,

                    long value => checked((int)value),

                    _ => throw new InvalidOperationException(
                        $"Unsupported retry count header type: {retryHeader?.GetType().Name}")
                };
            }

            bool isRetriesExhausted = retryCount >= retryOptions.Value.MaxRetries;

            var properties = new BasicProperties
            {
                Persistent = true,
                Headers = new Dictionary<string, object?>
                {
                    ["x-retry-count"] = retryCount + 1
                }
            };

            var routingKey = isRetriesExhausted
                ? RabbitMqRoutingKeys.DocumentProcessingDeadLetter
                : RabbitMqRoutingKeys.DocumentProcessingRetry;

            await channel.BasicPublishAsync(
                exchange: exchangeOptions.Value.Documents,
                routingKey: routingKey,
                mandatory: true,
                basicProperties: properties,
                body: eventArgs.Body,
                cancellationToken: cancellationToken
            );

            await channel.BasicAckAsync(
                deliveryTag: eventArgs.DeliveryTag,
                multiple: false,
                cancellationToken: cancellationToken
            );

            //
            if (isRetriesExhausted)
            {
                var document = await documentRepository.GetByIdAsync(documentId, cancellationToken);
                if (document is not null)
                {
                    document.MarkAsFailed();
                    await documentRepository.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}