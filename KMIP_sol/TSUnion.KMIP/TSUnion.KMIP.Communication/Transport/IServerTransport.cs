using System;
using System.Net;
using TSUnion.KMIP.Core.Response;
namespace TSUnion.KMIP.Communication.Transport
{
    public interface IServerTransport
    {
        void CreateServerTunnel(ConnectionDetails details);
        void RemoveServerTunnel();
        //void CreateClientTunnel(ConnectionDetails details);
        //void RemoveClientTunnel();
        event MessageReceivedEventHandler ServerRequestReceived;
        void SendResponseToClient(ResponseMessage response, IPAddress ip);
    }
}
