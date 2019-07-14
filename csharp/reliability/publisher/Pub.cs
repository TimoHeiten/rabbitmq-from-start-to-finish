using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using RabbitMQ.Client;
using System.Text;
using messages.Connection;
using System.Threading;

namespace reliability.publisher
{
 
    internal class Pub
    {
        public static void Run()
        {
            var conn = Stats.Create().CreateConnection();

            using (var channel = conn.CreateModel())
            {
                // confirm select
                channel.BasicAcks += (s, ea) => 
                {
                    System.Console.WriteLine($"broker acked msg: {ea.DeliveryTag}");
                };
                channel.BasicNacks += (s, ea) => 
                {
                    System.Console.WriteLine($"broker N-acked msg: {ea.DeliveryTag}");
                };
                channel.ConfirmSelect();

                for (int i = 0; i < 10; i++)
                {
                    var message = "hello publisher confirm - " + i;
                    var payload = Encoding.Unicode.GetBytes(message);
                    Console.WriteLine("Sending message: " + message);
                    channel.BasicPublish("", "any_non_existent_queue", null, payload);
                    Thread.Sleep(500);
                }
                channel.WaitForConfirmsOrDie();
            }
            System.Console.WriteLine("done!");
            conn.Close();
        }
    }
}