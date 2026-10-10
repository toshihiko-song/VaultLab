using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqQueueOptions
    {
        public string DocumentProcessing { get; set; } = string.Empty;
        public string DocumentProcessingRetry { get; set; } = string.Empty;
        public string DocumentProcessingDeadLetter { get; set; } = string.Empty;
    }
}