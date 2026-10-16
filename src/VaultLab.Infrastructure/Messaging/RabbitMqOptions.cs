namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqOptions
    {
        public int MaxRetries { get; set; }
        public int RetryDelayMilliseconds { get; set; } = 1000;
        public RabbitMqExchangesOptions Exchanges { get; set; } = new();
        public RabbitMqQueuesOptions Queues { get; set; } = new();
        public RabbitMqRoutingKeys RoutingKeys { get; set; } = new();
    }
}