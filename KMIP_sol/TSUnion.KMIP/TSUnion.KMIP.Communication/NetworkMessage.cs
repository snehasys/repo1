using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Communication
{
    [Serializable]
    public class NetworkMessage
    {
        public Guid ID { get; private set; }
        public IPAddress Source { get; set; }
        public IPAddress Destination { get; set; }
        public RequestMessage Request { get; set; }
        public ResponseMessage Response { get; set; }

        public NetworkMessage() 
        {
            ID = Guid.NewGuid();
        }
    }
}
