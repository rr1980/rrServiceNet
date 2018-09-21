

using System.Net.Sockets;
using rrServiceNet.GateServer;

internal abstract class CommandBase
{
    protected readonly Controller Controller;
    protected readonly Server Server;

    protected CommandBase(Controller controller, Server server)
    {
        this.Controller = controller;
        this.Server = server;
    }

    internal abstract void Execute(TcpClient client, int id, string[] param, string data);
}
