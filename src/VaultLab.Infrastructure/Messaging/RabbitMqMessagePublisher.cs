
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Messaging
{
    public class RabbitMqMessagePublisher : IMessagePublisher
    {
        private readonly RabbitMqConnection rabbitMqConnection;
        private readonly IOptions<RabbitMqOptions> rabbitMqOptions;

        public RabbitMqMessagePublisher(RabbitMqConnection rabbitMqConnection, IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            this.rabbitMqConnection = rabbitMqConnection;
            this.rabbitMqOptions = rabbitMqOptions;
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

            var rabbitMqExchanges = rabbitMqOptions.Value.Exchanges;
            var rabbitMqRoutingKeys = rabbitMqOptions.Value.RoutingKeys;

            await channel.BasicPublishAsync(
                exchange: rabbitMqExchanges.Document,
                routingKey: rabbitMqRoutingKeys.DocumentUploaded,
                mandatory: true,
                basicProperties: properties,
                body: body,
                cancellationToken: cancellationToken
            );
        }
    }
}