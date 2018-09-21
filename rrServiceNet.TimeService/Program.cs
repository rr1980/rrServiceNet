using rrServiceNet.BaseClient;
using rrServiceNet.Common;
using System;

namespace rrServiceNet.TimeService
{
    class Program
    {
        static Client client;

        static void Main(string[] args)
        {
            Console.Title = "TimeService";

            client = new Client();

            client.OnDataReceived += Client_OnDataReceived;
            client.ConnectToServer("127.0.0.1", 11000);


            CallPackage cp = new CallPackage();
            cp.Command = "register";
            cp.Data = "timeService";

            cp.Params.Add("commands", new[] { "time", "date" });

            Console.WriteLine(cp.Guid);
            client.Call(cp);

            string s = "";
            do
            {
                if (s == "call")
                {
                    cp = new CallPackage();
                    cp.Command = "call";
                    cp.Data = "time";
                    Console.WriteLine(cp.Guid);
                    client.Call(cp);
                }
                else
                {
                    client.Send(s);
                }
                Console.Write("command:>");
            } while ((s = Console.ReadLine()) != "exit");

            Console.ReadKey();
        }

        private static void Client_OnDataReceived(CallPackage response)
        {
            Console.WriteLine();
            Console.WriteLine("+: " + response.Data);
            Console.Write("command:>");

            if (response.Command == "call" && response.Data == "time")
                client.Call(new CallPackage
                {
                    Command = "response",
                    Data = DateTime.Now.ToString(),
                    Guid = response.Guid
                });

        }
    }
}
