using EasyNetQ;
using messages.Connection;

namespace messages.Messages
{
    [Queue(Stats.PIKA_QUEUE, ExchangeName = "")]
    public class PikaMessage
    {
        public PikaMessage(string text)
        {
            Content = text;
        }
        public string Content { get; set; }
    }
}