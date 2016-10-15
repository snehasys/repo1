using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum DigitalSignatureAlgorithmType
    {
        PKCS1_v15 = 0x00000001,
        MD5_RSA_PKCS1_v15 = 0x00000002,
        SHA1_RSA_PKCS1_v15 = 0x00000003,
        SHA224_RSA_PKCS1_v15 = 0x00000004,
        SHA256_RSA_PKCS1_v15 = 0x00000005,
        SHA384_RSA_PKCS1_v15 = 0x00000006,
        SHA512_RSA_PKCS1_v15 = 0x00000007,
        RSASSA_PSS_PKCS1_v21 = 0x00000008,
        DSA_SHA1 = 0x00000009,
        DSA_SHA224 = 0x0000000A,
        DSA_SHA256 = 0x0000000B,
        ECDSA_SHA1 = 0x0000000C,
        ECDSA_SHA224 = 0x0000000D,
        ECDSA_SHA256 = 0x0000000E,
        ECDSA_SHA384 = 0x0000000F,
        ECDSA_SHA512 = 0x00000010

    }
}
