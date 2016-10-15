using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum KeyCompressionType
    {
        ECPublicUncompressed = 00000001,
        ECPublicX962CompressedPrime = 00000002,
        ECPublicX962CompressedChar2 = 00000003,
        ECPublicX962Hybrid = 00000004,
        Extensions = 800000003

    }
}
