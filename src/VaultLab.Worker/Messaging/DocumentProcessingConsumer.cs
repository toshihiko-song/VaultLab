using System.Text;
using System.Text.Json;
using RabbitMQ.Client.Events;
using VaultLab.Domain.Entities;
using Microsoft.Extensions.Options;
using VaultLab.Infrastructure.Messaging;
using VaultLab.Application.Abstractions;
using VaultLab.Application.Contracts.Messaging;
using VaultLab.Infrastructure.Persistence;

namespace VaultLab.Worker.Messaging
{
    public sealed class DocumentProcessingConsumer(
        RabbitMqConnection rabbitMqConnection,
        IOptions<RabbitMqQueueOptions> queueOptions,
        IServiceScopeFactory scopeFactory
        )
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting document processing consumer...");

            var connection = await rabbitMqConnection.GetConnectionAsync(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            await channel.QueueDeclareAsync(
                queue: queueOptions.Value.DocumentProcessing,
                durable: true,
                exclusive: false,
                autoDelete: false,
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

                var textExtractor =
                    scope.ServiceProvider.GetRequiredService<IDocumentTextExtractor>();



                var fileStorage =
                        scope.ServiceProvider.GetRequiredService<IFileStorage>();


                var chunker =
                    scope.ServiceProvider.GetRequiredService<IDocumentChunker>();

                var embeddingGenerator =
                    scope.ServiceProvider.GetRequiredService<IEmbeddingGenerator>();


                Console.WriteLine($"Received document: {documentUploaded.DocumentId}");

                var document = await documentRepository.GetByIdAsync(documentUploaded.DocumentId, cancellationToken);


                if (document is null)
                {
                    Console.WriteLine($"Document is not found: {documentUploaded.DocumentId}");

                    return;
                }

                Console.WriteLine($"Processing document: {document.FileName}");

                try
                {
                    document.MarkAsProcessing();

                    await documentRepository.SaveChangesAsync(cancellationToken);

                    //Open Record File
                    await using var fileStream = await fileStorage.OpenReadAsync(document.StoragePath, cancellationToken);

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
                    Console.WriteLine($"Document processing failed: {ex.Message}");

                    document.MarkAsFailed();

                    await documentRepository.SaveChangesAsync(
                        cancellationToken);
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
    }
}