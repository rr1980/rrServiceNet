using System;

namespace rrServiceNet.GateServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            server.Start();

            Console.WriteLine("END");
            Console.ReadLine();
        }
    }
}
