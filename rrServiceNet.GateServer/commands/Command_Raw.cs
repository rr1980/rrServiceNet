

using rrServiceNet.Common;
using rrServiceNet.GateServer;
using System;

internal class Command_Raw : CommandBase
{
    public Command_Raw(Controller controller, Server server) : base(controller, server)
    {
    }

    internal override void Execute(CallPackage cp)
    {
        Console.WriteLine(cp.Data);
    }
}
