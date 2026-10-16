
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqInitializer(
        RabbitMqConnection rabbitMqConnection,
        IOptions<RabbitMqOptions> rabbitMqOptions
    )
    {
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var connection = await rabbitMqConnection.GetConnectionAsync(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            var exchanges = rabbitMqOptions.Value.Exchanges;
            var queues = rabbitMqOptions.Value.Queues;
            var routingKeys = rabbitMqOptions.Value.RoutingKeys;



            // Document Exchange
            await channel.ExchangeDeclareAsync(
                exchange: exchanges.Document,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken
            );


            // Declare Queues

            await channel.QueueDeclareAsync(
                queue: queues.DocumentProcessing,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: cancellationToken
            );

            await channel.QueueDeclareAsync(
                queue: queues.DocumentProcessingRetry,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: new Dictionary<string, object?>
                {
                    { "x-message-ttl", rabbitMqOptions.Value.RetryDelayMilliseconds },
                    { "x-dead-letter-exchange", exchanges.Document },
                    { "x-dead-letter-routing-key", routingKeys.DocumentUploaded }
                },
                cancellationToken: cancellationToken
            );

            await channel.QueueDeclareAsync(
                queue: queues.DocumentProcessingDeadLetter,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: cancellationToken
            );


            //Queue Bindings

            await channel.QueueBindAsync(
                queue: queues.DocumentProcessing,
                exchange: exchanges.Document,
                routingKey: routingKeys.DocumentUploaded,
                cancellationToken: cancellationToken
            );

            await channel.QueueBindAsync(
                queue: queues.DocumentProcessingRetry,
                exchange: exchanges.Document,
                routingKey: routingKeys.DocumentProcessingRetry,
                cancellationToken: cancellationToken
            );

            await channel.QueueBindAsync(
                queue: queues.DocumentProcessingDeadLetter,
                exchange: exchanges.Document,
                routingKey: routingKeys.DocumentProcessingDeadLetter,
                cancellationToken: cancellationToken
            );
        }
    }
}