using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using TSUnion.KMIP.Core.Enumerators;


namespace TSUnion.KMIP.Core.Request
{
    [Serializable]
    public class RequestMessage 
    {
       

        [Encoding(TagType.RequestHeader, DataType.Structure, true)]
        public RequestHeader Header { get; set; }

        [Encoding(TagType.BatchItem, DataType.Structure, true)]
        public RequestBatchItem BatchItem { get; set; }

        public int GetSize()
        {
            return Header.GetSize() + BatchItem.GetSize();
        }
        public Guid GetId() 
        {
            return (Guid)BatchItem.Payload.Attribute.Items[Constants.UID];
        }

     
        public void Push()
        {
              ILog Log = LogManager.GetLogger(typeof(RequestMessage));

            Log.Info("------------------------------------------------------");
            Log.Info("Request message info");
            Log.Info("Header[ProtocolVersion]=" + Header.ProtocolVersion.GetFullVersion());
            Log.Info("Header[MaximumResponseSize]=" + Header.MaximumResponseSize);
            Log.Info("Header[TimeStamp]=" + Header.TimeStamp);
            Log.Info("Header[AsynchronousIndicator]=" + Header.AsynchronousIndicator);
            Log.Info("------------------------------------------------------");


        }

       
    }
}
