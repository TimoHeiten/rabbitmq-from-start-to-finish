using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace consumer.mq_client
{
    internal class Consumer
    {
        public static void Run()
        {
            // connection
            IConnection connection = Create().CreateConnection();
            // channel
            using (var channel = connection.CreateModel()) // IModel
            {
                // basicConsume
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ch, eventArgs) => 
                {
                    var body = eventArgs.Body;
                    System.Console.WriteLine("message Received:");
                    System.Console.WriteLine(Encoding.UTF8.GetString(body));
                    channel.BasicAck(eventArgs.DeliveryTag, multiple:false);
                };
                string tag = channel.BasicConsume("pika_queue", autoAck:false, consumer);
                Console.WriteLine("connection established! Consuming in the background!");
                Console.ReadKey();
            }
        }

        private static ConnectionFactory Create()
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