using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Infrastructure.Messaging
{
    public static class RabbitMqRoutingKeys
    {
        public const string DocumentUploaded = "document.uploaded";
        public const string DocumentProcessingRetry = "document.processing.retry";
        public const string DocumentProcessingDeadLetter = "document.processing.deadletter";
    }
}