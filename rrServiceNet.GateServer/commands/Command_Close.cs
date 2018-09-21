

using System.Net.Sockets;
using rrServiceNet.GateServer;

internal class Command_Close : CommandBase
{
    public Command_Close(Controller controller, Server server) : base(controller, server)
    {
    }

    internal override void Execute(TcpClient client, int id, string[] param, string data)
    {
        Server.Send(client, "go exit: " + id);

        client.Client.Shutdown(SocketShutdown.Both);
        client.Close();
    }
}
