using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum KeyCryptographicAlgorithmType
    {
        DES = 00000001,
        DES3 = 00000002,
        AES = 00000003,
        RSA = 00000004,
        DSA = 00000005,
        ECDSA = 00000006,
        HMAC_SHA1 = 00000007,
        HMAC_SHA224 = 00000008,
        HMAC_SHA256 = 00000009,
        HMAC_SHA384 = 0x0000000A,
        HMAC_SHA512 = 0x0000000B,
        HMAC_MD5 = 0x0000000C,
        DH = 0x0000000D,
        ECDH = 0x0000000E,
        ECMQV = 0x0000000F,
        Blowfish = 00000010,
        Camellia = 00000011,
        CAST5 = 00000012,
        IDEA = 00000013,
        MARS = 00000014,
        RC2 = 00000015,
        RC4 = 00000016,
        RC5 = 00000017,
        SKIPJACK = 00000018,
        Twofish = 00000019,
        Extensions = 80000001
    }
}

