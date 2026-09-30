
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqInitializer(RabbitMqConnection rabbitMqConnection, IOptions<RabbitMqQueueOptions> queueOptions)
    {
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var connection = await rabbitMqConnection.GetConnectionAsync(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            await channel.QueueDeclareAsync(
                queue: queueOptions.Value.DocumentProcessing,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: cancellationToken
            );
        }
    }
}