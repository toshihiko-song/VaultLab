using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqRetryOptions
    {
        public int MaxRetries { get; set; }
        public int RetryDelayMilliseconds { get; set; } = 1000;
    }
}