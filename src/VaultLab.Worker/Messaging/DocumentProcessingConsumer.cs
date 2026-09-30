using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using VaultLab.Application.Contracts.Messaging;
using VaultLab.Infrastructure.Messaging;

namespace VaultLab.Worker.Messaging
{
public sealed class DocumentProcessingConsumer(RabbitMqConnection rabbitMqConnection, IOptions<RabbitMqQueueOptions> queueOptions)
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

                if(documentUploaded is null)
                    return;

                Console.WriteLine($"Received document: {documentUploaded.DocumentId}");

                await channel.BasicAckAsync(
                    eventArgs.DeliveryTag,
                    multiple: false,
                    cancellationToken: cancellationToken
                );
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