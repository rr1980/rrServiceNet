using rrServiceNet.Common;
using System.Collections.Generic;
using System.Linq;

namespace rrServiceNet.GateServer
{
    internal class Controller
    {
        readonly Dictionary<string[], CommandBase> commands = new Dictionary<string[], CommandBase>();

        private Server server;

        public Controller(Server server)
        {
            this.server = server;

            commands.Add(new[] { "raw" }, new Command_Raw(this, server));
            commands.Add(new[] { "register", "call", "response" }, new Command_Register(this, server));
            commands.Add(new[] { "close" }, new Command_Close(this, server));
        }

        internal void Handle(CallPackage cp)
        {
            var com = commands.FirstOrDefault(x => x.Key.Any(y => y == cp.Command)).Value;

            if (com != null)
            {
                com.Execute(cp);
            }
        }

        internal void AddComman(string key)
        {

        }
    }
}
