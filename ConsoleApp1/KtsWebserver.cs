using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Collections;
using ConsoleApp1;

namespace sepashttpserver
{
    class KtsWebserver
    {
        public Boolean runserver = false;



        public  void start_server()
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            var prefixes = new List<string>() { "http://localhost:8080" +"/"};

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");
            while (runserver)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                Console.WriteLine("url == " + request.Url);
                Console.WriteLine(documentContents);

                HttpListenerResponse response = context.Response;

                if (request.HttpMethod == "OPTIONS")
                {
                    response.AddHeader("Access-Control-Allow-Headers", "*");
                }
                response.AppendHeader("Access-Control-Allow-Origin", "*");

                if (request.HttpMethod.Equals("GET"))
                {
                    new GetRequestHandler().process(request, response);
                    continue;
                }

                string responseString = "service up";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            listener.Stop();
        }
    }
}
