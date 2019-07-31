using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace endpoints
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:15672/api/exchanges/your_named_host";

            var request = WebRequest.Create(url);
            request.Credentials = new Creds().GetCredential(new Uri("http://localhost/"),"");
            var response = request.GetResponse();

            string result = "";
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            JToken token = JToken.Parse(result);
            foreach (var item in token)
            {
                System.Console.WriteLine(item);
            }
            Console.ReadKey();
        }

        public class Creds : ICredentials
        {
            public NetworkCredential GetCredential(Uri uri, string authType)
            {
                return new NetworkCredential("guest", "guest");
            }
        }
    }
}
