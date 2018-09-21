using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace rrServiceNet.GateServer
{
    internal class Controller
    {
        readonly Dictionary<string, CommandBase> commands = new Dictionary<string, CommandBase>();

        private Server server;

        public Controller(Server server)
        {
            this.server = server;

            commands.Add("register", new Command_Register(this, server));
            commands.Add("close", new Command_Close(this, server));
        }

        internal void Handle(TcpClient client, int id, string data)
        {
            Console.WriteLine(data);
            var param = data.Split(";");

            if (commands.TryGetValue(param[0], out var com))
            {
                com.Execute(client, id, param, data);
            }

            if (data.StartsWith("register"))
            {
            }
        }
    }
}
