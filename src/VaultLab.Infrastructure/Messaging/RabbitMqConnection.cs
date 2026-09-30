using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace VaultLab.Infrastructure.Messaging
{
    public sealed class RabbitMqConnection(IOptions<RabbitMqOptions> options)
    {
        private readonly ConnectionFactory connectionFactory = new()
        {
            HostName = options.Value.HostName,
            UserName = options.Value.UserName,
            Password = options.Value.Password
        };

        private IConnection? connection;

        public async Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
        {
            if(connection is not null && connection.IsOpen)
                return connection;
            
            connection = await connectionFactory.CreateConnectionAsync(cancellationToken);

            return connection;
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}