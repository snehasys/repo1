using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum DerivationMethodType
    {
        PBKDF2 = 0x00000001,
        HASH = 0x00000002,
        HMAC = 0x00000003,
        ENCRYPT = 0x00000004,
        NIST800_108_C = 0x00000005,
        NIST800_108_F = 0x00000006,
        NIST800_108_DPI = 0x00000007
    }
}
