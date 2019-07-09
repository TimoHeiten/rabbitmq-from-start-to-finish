using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace messages.Connection
{
    public static class Stats
    {
        public static ConnectionFactory Create()
        {
            return new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                Protocol = Protocols.DefaultProtocol
            };
        }
    }
}