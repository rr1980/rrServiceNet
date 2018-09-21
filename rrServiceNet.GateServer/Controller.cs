using System.Collections.Generic;

namespace rrServiceNet.GateServer
{
    internal class Controller
    {
        readonly Dictionary<string, CommandBase> commands = new Dictionary<string, CommandBase>();

        private Server server;

        public Controller(Server server)
        {
            this.server = server;

            commands.Add("raw", new Command_Raw(this, server));
            commands.Add("register", new Command_Register(this, server));
            commands.Add("close", new Command_Close(this, server));
        }

        internal void Handle(CallPackage cp)
        {
            if (commands.TryGetValue(cp.Command, out var com))
            {
                com.Execute(cp);
            }
        }
    }
}
