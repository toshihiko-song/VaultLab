
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Messaging
{
    public class RabbitMqMessagePublisher : IMessagePublisher
    {
        private readonly RabbitMqConnection rabbitMqConnection;
        private readonly IOptions<RabbitMqQueueOptions> queueOptions;

        public RabbitMqMessagePublisher(RabbitMqConnection rabbitMqConnection, IOptions<RabbitMqQueueOptions> queueOptions)
        {
            this.rabbitMqConnection = rabbitMqConnection;
            this.queueOptions = queueOptions;
        }
        public async Task PublishMessage<T>(T message, CancellationToken cancellationToken = default)
        {
            await using var connection =
                await rabbitMqConnection.GetConnectionAsync(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            var body = JsonSerializer.SerializeToUtf8Bytes(message);


            var properties = new BasicProperties
            {
                ContentType = "application/json",
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: queueOptions.Value.DocumentProcessing,
                mandatory:   true,
                basicProperties: properties,
                body: body,
                cancellationToken: cancellationToken
            );
        }
    }
}