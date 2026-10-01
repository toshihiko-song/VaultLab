using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqExchangeOptions
    {
        public string Documents { get; set; } = string.Empty;
    }
}