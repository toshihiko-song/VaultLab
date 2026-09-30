using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Worker.Messaging
{
    public class DocumentProcessingWorker(DocumentProcessingConsumer consumer) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await consumer.StartAsync(stoppingToken);
        }
    }
}