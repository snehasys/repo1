using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.Common
{
    [Serializable]
    public class ProtocolVersionStructure
    {
        [Encoding(TagType.ProtocolVersionMajor, DataType.Integer,true)]
        public Int32 ProtocolVersionMajor { get; set; }
        [Encoding(TagType.ProtocolVersionMinor, DataType.Integer,true)]
        public Int32 ProtocolVersionMinor { get; set; }

        public int GetSize()
        {
            return sizeof(Int32) + sizeof(Int32);
        }

        internal string GetFullVersion()
        {
          return("0.0."+ProtocolVersionMajor+"."+ProtocolVersionMinor);
        }
    }
}
