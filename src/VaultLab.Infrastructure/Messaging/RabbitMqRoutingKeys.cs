using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqRoutingKeys
    {
        public string DocumentUploaded = string.Empty;
        public string DocumentProcessingRetry = string.Empty;
        public string DocumentProcessingDeadLetter = string.Empty;
    }
}