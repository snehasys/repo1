using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum BlockCipherModeType
    {
        CBC = 0x00000001,
        ECB = 0x00000002,
        PCBC = 0x00000003,
        CFB = 0x00000004,
        OFB = 0x00000005,
        CTR = 0x00000006,
        CMAC = 0x00000007,
        CCM = 0x00000008,
        GCM = 0x00000009,
        CBC_MAC = 0x0000000A,
        XTS = 0x0000000B,
        AESKeyWrapPadding = 0x0000000C,
        NISTKeyWrap = 0x0000000D,
        X9_102_AESKW = 0x0000000E,
        X9_102_TDKW = 0x0000000F,
        X9_102_AKW1 = 0x00000010,
        X9_102_AKW2 = 0x0000001,
        Extensions = 0x8000001

    }
}
