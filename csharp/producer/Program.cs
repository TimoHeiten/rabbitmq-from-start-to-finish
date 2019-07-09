using System;
using System.Collections.Generic;
using System.Threading;

namespace producer
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = string.Empty;
            try
            {   
                key = args[1];
                s_runner[key].Invoke();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            System.Console.WriteLine("-".PadRight(50));
            System.Console.WriteLine($"ran with key: {key}");
            Thread.Sleep(500);
        }

         static Program()
        {
            s_runner = new Dictionary<string, Action>
            {
                ["produce"] = producer.mq_client.Producer.Run
            };
        }

        private static Dictionary<string, Action> s_runner;
    }
}
