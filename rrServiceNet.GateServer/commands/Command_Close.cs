using rrServiceNet.Common;
using rrServiceNet.GateServer;

internal class Command_Close : CommandBase
{
    public Command_Close(Controller controller, Server server) : base(controller, server)
    {
    }

    internal override void Execute(CallPackage cp)
    {
        Server.Send(cp.Client, "go exit");
        Server.DisconnectClient(cp.Client);
    }
}
