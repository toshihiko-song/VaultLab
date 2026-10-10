
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqInitializer(
        RabbitMqConnection rabbitMqConnection,
        IOptions<RabbitMqQueueOptions> queueOptions,
        IOptions<RabbitMqExchangeOptions> exchangeOptions,
        IOptions<RabbitMqRetryOptions> retryOptions
    )
    {
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var connection = await rabbitMqConnection.GetConnectionAsync(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            var documentExchange = exchangeOptions.Value.Documents;
            var queues = queueOptions.Value;

            // Document Exchange
            await channel.ExchangeDeclareAsync(
                exchange: documentExchange,
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
                    { "x-message-ttl", retryOptions.Value.RetryDelayMilliseconds },
                    { "x-dead-letter-exchange", documentExchange },
                    { "x-dead-letter-routing-key", RabbitMqRoutingKeys.DocumentUploaded }
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
                exchange: documentExchange,
                routingKey: RabbitMqRoutingKeys.DocumentUploaded,
                cancellationToken: cancellationToken
            );

            await channel.QueueBindAsync(
                queue: queues.DocumentProcessingRetry,
                exchange: documentExchange,
                routingKey: RabbitMqRoutingKeys.DocumentProcessingRetry,
                cancellationToken: cancellationToken
            );

            await channel.QueueBindAsync(
                queue: queues.DocumentProcessingDeadLetter,
                exchange: documentExchange,
                routingKey: RabbitMqRoutingKeys.DocumentProcessingDeadLetter,
                cancellationToken: cancellationToken
            );
        }
    }
}