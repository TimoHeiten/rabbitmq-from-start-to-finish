using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using RabbitMQ.Client;
using System.Text;

namespace producer.mq_client
{
    internal class Producer
    {
        public static void Run()
        {
            // connect
            var connection = messages.Connection.Stats.Create().CreateConnection();
            // create channel
            using (var channel = connection.CreateModel())
            {
                string input = "";
                while (input != "q")
                {
                    System.Console.WriteLine("start publishing message");

                    // basic publish
                    string body = "hello from .net publisher!";
                    channel.BasicPublish("", "pika_queue", mandatory:false
                        , basicProperties: null, body: Encoding.UTF8.GetBytes(body));

                    System.Console.WriteLine("published!");
                    input = Console.ReadLine();
                }
            }
            connection.Close(); // or via using!
        }
    }
}