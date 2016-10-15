using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Common;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.Response
{
    [Serializable]
    public class ResponseHeader
    {

        [Encoding(TagType.ProtocolVersion, DataType.Structure, true)]
     
        public ProtocolVersionStructure ProtocolVersion { get; set; }

        [Encoding(TagType.TimeStamp, DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [Encoding(TagType.BatchCount, DataType.Integer, true)]
        public Int32 BatchCount { get; set; }


    }
}
