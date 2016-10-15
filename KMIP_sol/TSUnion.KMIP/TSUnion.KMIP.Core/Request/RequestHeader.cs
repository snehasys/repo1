using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Common;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.Request
{
    [Serializable]
    public class RequestHeader
    {
        [Encoding(TagType.ProtocolVersion, DataType.Structure, true)]
        public ProtocolVersionStructure ProtocolVersion { get; set; }

        [Encoding(TagType.MaximumResponseSize, DataType.Integer)]
        public Int32 MaximumResponseSize { get; set; }

        [Encoding(TagType.AsynchronousIndicator, DataType.Boolean)]
        public Boolean AsynchronousIndicator { get; set; }

        [Encoding(TagType.Authentication, DataType.Structure)]
        public CredentialStructure Authentication { get; set; }

        [Encoding(TagType.BatchErrorContinuationOption, DataType.Enumeration)]
        public BatchErrorContinuationOptionType BatchErrorContinuationOption { get; set; }

        [Encoding(TagType.BatchOrderOption, DataType.Boolean)]
        public Boolean BatchOrderOption { get; set; }

        [Encoding(TagType.TimeStamp, DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [Encoding(TagType.BatchCount, DataType.Integer, true)]
        public Int32 BatchCount { get; set; }


        public int GetSize()
        {
            return ProtocolVersion.GetSize() + 8 +//
                    sizeof(Int32) + 8 +       //MaximumResponseSize
                    sizeof(Boolean) + 8 +
                    Authentication.GetSize() + 8 + //AsynchronousIndicator
                    sizeof(Int32) + 8 + //BatchErrorContinuationOption
                    sizeof(Boolean) + 8 + //BatchOrderOption
                    sizeof(Int32) + 8 +//BatchCount
                    sizeof(Int32) + 8; // TimeStamp



        }
    }
}
