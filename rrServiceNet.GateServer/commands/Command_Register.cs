

using rrServiceNet.GateServer;
using System;

internal class Command_Register : CommandBase
{
    public Command_Register(Controller controller, Server server) : base(controller, server)
    {
    }

    internal override void Execute(CallPackage cp)
    {
        Console.WriteLine(cp.Guid);
        Server.Send(cp.Client, "registred by server");
    }
}
