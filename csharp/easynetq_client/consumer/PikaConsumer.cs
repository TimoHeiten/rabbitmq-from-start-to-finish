using System;
using System.Threading.Tasks;
using EasyNetQ;
using messages.Connection;
using System.Text;
using messages.Messages;
using EasyNetQ.Topology;

namespace consumer
{
    internal class PikaConsumer
    {
        public static void Consume()
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                var advanced = bus.Advanced;
                IQueue pika_queue = advanced.QueueDeclare(Stats.PIKA_QUEUE);

                advanced.Consume(pika_queue, OnMessage);
                Console.ReadKey();
            }
        }
        private static Task OnMessage(byte[] bytes, MessageProperties properties, MessageReceivedInfo info)
        {
            string result = Encoding.UTF8.GetString(bytes);
            System.Console.WriteLine(result);

            return Task.CompletedTask;
        }
    }
}