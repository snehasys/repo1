using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Common;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.Request
{
    [Serializable]
    public class RequestBatchItem
    {
        [Encoding(TagType.Operation, DataType.Enumeration, true)]
        public OperationType Operation { get; set; }

        [Encoding(TagType.UniqueBatchItemID, DataType.ByteString)]
        public string UniqueBatchItemID { get; set; }

        [Encoding(TagType.RequestPayload, DataType.Structure, true)]
        public RequestPayload Payload { get; set; }

        [Encoding(TagType.MessageExtension, DataType.Structure, true)]
        public MessageExtensionStructure MessageExtension { get; set; }


        internal int GetSize()
        {
            return 0;
        }
    }
}
