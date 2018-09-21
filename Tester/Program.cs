using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Connect();
                Console.WriteLine("connect again?");
            } while (Console.ReadLine() != "exit");

            Console.ReadKey();
        }

        private static void Connect()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 11000;
            TcpClient client = new TcpClient();

            try
            {
                client.Connect(ip, port);
            }
            catch (Exception ex)
            {
                // client.Client.Shutdown(SocketShutdown.Send);
                client.Close();
                Console.WriteLine("client NOT connected!!");
                return;
            }

            Console.WriteLine("client connected!!");
            NetworkStream ns = client.GetStream();
            Thread thread = new Thread(o => ReceiveData((TcpClient)o));

            thread.Start(client);

            // byte[] buffer = Encoding.ASCII.GetBytes("register");
            // ns.Write(buffer, 0, buffer.Length);

            string s = "register";
            do
            {
                byte[] buffer = Encoding.ASCII.GetBytes(s);
                ns.Write(buffer, 0, buffer.Length);
            } while (!string.IsNullOrEmpty((s = Console.ReadLine())));

            client.Client.Shutdown(SocketShutdown.Send);
            thread.Join();
            ns.Close();
            client.Close();
            Console.WriteLine("disconnect from server!!");
        }

        static void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            try
            {
                while (client.Connected && ns.CanRead && (byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
                {
                    Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("disconnect from server!!");
            }
        }
        //static void Main(string[] args)
        //{
        //    AsynchronousClient client = new AsynchronousClient();

        //    client.StartClient();

        //    Console.WriteLine("END");
        //    Console.ReadLine();
        //}
    }
}
