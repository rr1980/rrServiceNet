

using System.Net.Sockets;
using rrServiceNet.GateServer;

internal class Command_Register : CommandBase
{
    public Command_Register(Controller controller, Server server) : base(controller, server)
    {
    }

    internal override void Execute(TcpClient client, int id, string[] param, string data)
    {
        Server.Send(client, "registred with id: " + id);
    }
}