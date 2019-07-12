using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using RabbitMQ.Client;
using messages.Connection;
using System.Text;

namespace properties
{
    public class Producer
    {
        public static void Run()
        {
             // connect
            var connection = messages.Connection.Stats.Create().CreateConnection();
            // create channel
            using (var channel = connection.CreateModel())
            {
                System.Console.WriteLine("enter any message pls");
                string input = "message";
                while (input != "q")
                {
                    byte[] message = Encoding.UTF8.GetBytes(input);
                    IBasicProperties props = channel.CreateBasicProperties();
                    props.ContentType = "text/plain";
                    props.DeliveryMode = 2; // means persistent
                    props.Expiration = "5000"; // or 5 seconds
                    channel.BasicPublish("amq.direct", "ttl", props, message);
                   
                    System.Console.WriteLine("published... enter next message");
                    input = Console.ReadLine();
                }
            }
            connection.Close();
        }
    }
}