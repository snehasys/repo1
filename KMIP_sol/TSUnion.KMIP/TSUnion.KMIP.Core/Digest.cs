using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class Digest
    {
        public HashingAlgorithmType Algorithm { get; set; }
        public byte[] Value { get; set; }
        public KeyFormatType KeyFormat { get; set; }



    }
}
