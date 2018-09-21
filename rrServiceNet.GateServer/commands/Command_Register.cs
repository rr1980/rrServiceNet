

using Newtonsoft.Json.Linq;
using rrServiceNet.Common;
using rrServiceNet.GateServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

internal class Command_Register : CommandBase
{
    readonly Dictionary<string[], TcpClient> commands = new Dictionary<string[], TcpClient>();
    readonly Dictionary<Guid, TcpClient> awaitResponses = new Dictionary<Guid, TcpClient>();

    public Command_Register(Controller controller, Server server) : base(controller, server)
    {
    }

    internal override void Execute(CallPackage cp)
    {
        Console.WriteLine(cp.Guid);

        switch (cp.Command)
        {
            case "register":
                Register(cp);
                break;
            case "call":
                Call(cp);
                break;
            case "response":
                Response(cp);
                break;
        }


        //string[] commands = (string[])cp.Params.FirstOrDefault(x => x.Key == "commands").Value;

    }

    private void Register(CallPackage cp)
    {
        var d = cp.Params.FirstOrDefault(x => x.Key == "commands").Value;
        if (d != null)
        {
            JArray jsonResponse = JArray.Parse(d.ToString());
            commands.Add(jsonResponse.ToObject<string[]>(), cp.Client);
        }

        Server.Send(cp.Client, "registred by server");
    }

    private void Call(CallPackage cp)
    {
        var _client = commands.FirstOrDefault(x => x.Key.Any(y => y == cp.Data)).Value;
        awaitResponses.Add(cp.Guid, cp.Client);
        Server.Call(cp, _client);
        Console.WriteLine("call " + cp.Data);
    }

    private void Response(CallPackage cp)
    {
        var _client = awaitResponses.FirstOrDefault(x => x.Key == cp.Guid).Value;

        Server.Call(cp, _client);

        Console.WriteLine("call " + cp.Data);
    }
}
