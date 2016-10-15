using System;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;
namespace TSUnion.KMIP.Communication.Transport
{
   public interface IClientTransport
    {
        
        void Connect(ConnectionDetails details, bool delay);
        void Connect(ConnectionDetails details);
        void Close();
        Guid SendRequest(RequestMessage message);
        ResponseMessage WaitForResponse();
        
       
        

        
    }
}
