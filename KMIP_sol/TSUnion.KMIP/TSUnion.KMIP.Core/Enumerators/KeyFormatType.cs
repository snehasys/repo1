using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum KeyFormatType

    {
        Raw = 0x00000001,
        Opaque = 0x00000002,
        PKCS1 = 0x00000003,
        PKCS8 = 0x00000004,
        X509 = 0x00000005,
        ECPrivateKey = 0x00000006,
        TransparentSymmetricKey = 0x00000007,
        TransparentDSAPrivateKey = 0x00000008,
        TransparentDSAPublicKey = 0x00000009,
        TransparentRSAPrivateKey = 0x0000000A,
        TransparentRSAPublicKey = 0x0000000B,
        TransparentDHPrivateKey = 0x0000000C
    }
}
