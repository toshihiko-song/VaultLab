
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqInitializer(
        RabbitMqConnection rabbitMqConnection,
        IOptions<RabbitMqQueueOptions> queueOptions,
        IOptions<RabbitMqExchangeOptions> exchangeOptions
    )
    {
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var connection = await rabbitMqConnection.GetConnectionAsync(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            // Document Exchange
            await channel.ExchangeDeclareAsync(
                exchange: exchangeOptions.Value.Documents,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken
            );

            // Document Processing Queue

            await channel.QueueDeclareAsync(
                queue: queueOptions.Value.DocumentProcessing,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: cancellationToken
            );

            //Queue Bindings

            await channel.QueueBindAsync(
                queue: queueOptions.Value.DocumentProcessing,
                exchange: exchangeOptions.Value.Documents,
                routingKey: RabbitMqRoutingKeys.DocumentUploaded,
                cancellationToken: cancellationToken
            );


        }
    }
}