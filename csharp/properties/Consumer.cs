using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using RabbitMQ.Client.Events;
using System.Text;

namespace properties
{
 
    public class Consumer
    {
        public static void Run()
        {
             // connect
            var connection = messages.Connection.Stats.Create().CreateConnection();
            // create channel
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (c, ea) => 
                {
                    var body = ea.Body;
                    string message = Encoding.UTF8.GetString(body);
                    System.Console.WriteLine(message);
                    channel.BasicAck(ea.DeliveryTag, false);
                };
                string queue = "expiring_queue";
                var dictionary = new Dictionary<string, object>
                {
                    ["x-expires"] = 7500
                };
                channel.QueueDeclare(queue, true, false, false, dictionary);
                channel.QueueBind(queue, "amq.direct", "ttl", null);
                channel.BasicConsume(queue, false, "tag", false, false , null, consumer);

                System.Console.WriteLine("press any key to exit");
                Console.ReadKey();
            }
            connection.Close();
        }
    }
}