using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.Response
{
    [Serializable]
    public class ResponseMessage
    {
        [Encoding(TagType.ResponseHeader, DataType.Structure, true)]
        public ResponseHeader Header { get; set; }

        [Encoding(TagType.BatchItem, DataType.Structure, true)]
        public ResponseBatchItem BatchItem { get; set; }
        public bool IsIncludeManagedObject()
        {
            if (BatchItem.ResponsePayload.Attribute != null)
            {
                return BatchItem.ResponsePayload.Attribute.Items.ContainsKey(Constants.CRYPTO_OBJECT);
            }
            else return false;
        }


        public int Size
        {
            get
            {

                {
                    return TransportObjectHelper.GetSize(this);
                }
            }
        }

        public BaseObject GetIncludedManagedObject()
        {
            if (IsIncludeManagedObject())
                return BatchItem.ResponsePayload.Attribute.Items[Constants.CRYPTO_OBJECT] as BaseObject;
            else return null;
        }
        public void Push()
        {

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Response message info");
            Console.WriteLine("Header[ProtocolVersion]=" + Header.ProtocolVersion.GetFullVersion());
            Console.WriteLine("Header[TimeStamp]=" + Header.TimeStamp);
            Console.WriteLine("Header[BatchCount]=" + Header.BatchCount);
            Console.WriteLine("------------------------------------------------------");


        }


        public Guid GetFirstReturnedId()
        {
            return BatchItem.ResponsePayload.UniqueIdentifier.Single();
        }
    }
}
