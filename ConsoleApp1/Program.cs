// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using Newtonsoft.Json;
using sepashttpserver;
using System.Collections.Generic;
using System.Net;

Console.WriteLine("Hello, World!");



KtsWebserver server = new KtsWebserver();
server.runserver = true;
server.start_server();

//var ws = new WebServer(SendResponse, "http://localhost:8080/test/");
//ws.Run();
//Console.WriteLine("A simple webserver. Press a key to quit.");
//Console.ReadKey();
//ws.Stop();




//static string SendResponse(HttpListenerRequest request)
//{


//  var json = JsonConvert.SerializeObject(list);

// return json;

//}