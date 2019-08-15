using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using RabbitMQ.Client;
using System.Text;
using messages.Connection;
using System.Threading;
using RabbitMQ.Client.Events;

namespace reliability.publisher
{
 
    internal class Consumer
    {
        public static void Run()
        {
            var conn = Stats.Create().CreateConnection();
            using (var channel = conn.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                Action<BasicGetResult>  action = (bgresult) => 
                {
                    var body = bgresult.Body;
                    System.Console.WriteLine("message Received:");
                    System.Console.WriteLine(Encoding.UTF8.GetString(body));
                    // 
                    channel.BasicAck(bgresult.DeliveryTag, multiple:false);
                };
                BasicGetResult result = channel.BasicGet(Stats.PIKA_QUEUE, false);
                action(result);
            }
            System.Console.WriteLine("done!");
            conn.Close();
        }
    }
}