using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum HashingAlgorithmType
    {
        MD2 = 0x00000001,
        MD4 = 0x00000002,
        MD5 = 0x00000003,
        SHA_1 = 0x00000004,
        SHA_224 = 0x00000005,
        SHA_256 = 0x00000006,
        SHA_384 = 0x00000007,
        SHA_512 = 0x00000008,
        RIPEMD_160 = 0x00000009,
        Tiger = 0x0000000A,
        Whirlpool = 0x0000000B

    }
}
