using rrServiceNet.Common;
using System;

namespace rrServiceNet.GateServer
{
    class Program
    {
        static Server server;
        static Controller controller;
        static void Main(string[] args)
        {
            Console.Title = "GateServer";

            server = new Server(11000);
            controller = new Controller(server);

            server.OnDataReceived += Server_OnDataReceived;


            server.Start();
            do
            {
                Console.Write("command:>");
            } while (Console.ReadLine() != "exit");

            server.Stop();

            Console.WriteLine("END");
            Console.ReadLine();
        }

        private static void Server_OnDataReceived(CallPackage cp)
        {
            Console.WriteLine();
            Console.WriteLine("+: command: " + cp.Command);

            controller.Handle(cp);
            Console.Write("command:>");
        }
    }
}
