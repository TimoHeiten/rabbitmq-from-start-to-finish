using System;
using System.Text;
using EasyNetQ;
using EasyNetQ.Topology;
using messages.Connection;

namespace producer
{
    internal class PikaProducer
    {
        public static void Produce()
        {
           using (var bus = RabbitHutch.CreateBus("host=localhost"))
           {
               for (int i = 0; i < 3; i++)
               {
                   string body = $"message No {i}";
                   byte[] bytes = Encoding.UTF8.GetBytes(body);

                   var properties = new MessageProperties();
                   bus.Advanced.Publish(
                       Exchange.GetDefault(),
                       Stats.PIKA_QUEUE,
                       false,
                       properties,
                       bytes);
               }
               System.Console.WriteLine("publishing done!");
           }
        }
    }
}