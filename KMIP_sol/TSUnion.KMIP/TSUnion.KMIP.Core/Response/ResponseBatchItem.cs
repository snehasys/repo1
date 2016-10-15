using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Common;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.Request;

namespace TSUnion.KMIP.Core.Response
{
    [Serializable]
   public class ResponseBatchItem
    {
        [Encoding(TagType.Operation, DataType.Enumeration, true)]
        public OperationType Operation { get; set; }

        [Encoding(TagType.UniqueBatchItemID, DataType.ByteString)]
        public Guid UniqueBatchItemID { get; set; }

        [Encoding(TagType.ResultStatus, DataType.Enumeration,true)]
        public ResultStatusType ResultStatus { get; set; }

        [Encoding(TagType.ResultReason, DataType.Enumeration)]
        public ResultReasonType ResultReason { get; set; }


        [Encoding(TagType.ResultMessage, DataType.TextString)]
        public string ResultMessage { get; set; }

        [Encoding(TagType.AsynchronousCorrelationValue, DataType.ByteString)]
        public string AsynchronousCorrelationValue { get; set; }

        [Encoding(TagType.ResponsePayload, DataType.Structure, true)]
        public ResponsePayloadType ResponsePayload { get; set; }

        [Encoding(TagType.MessageExtension, DataType.Structure, true)]
        public MessageExtensionStructure MessageExtension { get; set; }
    }
}
