using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace rrServiceNet.GateServer
{
    internal class Server
    {
        readonly object _lock = new object();
        readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
        readonly Controller _controller;

        public Server()
        {
            _controller = new Controller(this);
        }

        internal void Start()
        {
            int count = 1;

            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 11000);
            ServerSocket.Start();

            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();
                lock (_lock) list_clients.Add(count, client);
                Console.WriteLine("connect...: " + client.Client.LocalEndPoint);

                Thread t = new Thread(handle_clients);
                t.Start(count);
                count++;
            }
        }

        private void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;

            lock (_lock) client = list_clients[id];

            var run = true;

            while (run)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int byte_count = stream.Read(buffer, 0, buffer.Length);

                    if (byte_count == 0)
                    {
                        break;
                    }

                    string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                    _controller.Handle(client, id, data);
                }
                catch (Exception es)
                {
                    Console.WriteLine("disconnected...: " + id);
                    run = false;
                }
            }

            lock (_lock) list_clients.Remove(id);

            try
            {
                client.Client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception es)
            {
                client.Close();
            }
        }

        internal void Broadcast(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (_lock)
            {
                foreach (TcpClient c in list_clients.Values)
                {
                    NetworkStream stream = c.GetStream();

                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        internal void Send(TcpClient client, string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (_lock)
            {
                NetworkStream stream = client.GetStream();

                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
