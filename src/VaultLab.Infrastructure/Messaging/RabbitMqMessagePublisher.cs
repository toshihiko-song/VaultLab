
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Messaging
{
    public class RabbitMqMessagePublisher : IMessagePublisher
    {
        private readonly ConnectionFactory connectionFactory;

        public RabbitMqMessagePublisher(IOptions<RabbitMqOptions> options)
        {
            var settings = options.Value;

            connectionFactory = new ConnectionFactory
            {
                HostName = settings.HostName,
                UserName = settings.UserName,
                Password = settings.Password
            };
        }
        public async Task PublishMessage<T>(T message, CancellationToken cancellationToken = default)
        {
            await using var connection =
                await connectionFactory.CreateConnectionAsync(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            var body = JsonSerializer.SerializeToUtf8Bytes(message);


            var properties = new BasicProperties
            {
                ContentType = "application/json",
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: "document-processing",
                mandatory:   true,
                basicProperties: properties,
                body: body,
                cancellationToken: cancellationToken
            );
        }
    }
}