using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum PaddingMethodType
    {
        None = 0x00000001,
        OAEP = 0x00000002,
        PKCS5 = 0x00000003,
        SSL3 = 0x00000004,
        Zeros = 0x00000005,
        ANSI_X9_23 = 0x00000006,
        ISO_10126 = 0x00000007,
        PKCS1_v15 = 0x00000008,
        X9_31 = 0x00000009,
        PSS = 0x0000000A
    }
}
