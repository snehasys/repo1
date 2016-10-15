using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum CurveType
    {
        P_192 = 0x00000001,
        K_163 = 0x00000002,
        B_163 = 0x00000003,
        P_224 = 0x00000004,
        K_233 = 0x00000005,
        B_233 = 0x00000006,
        P_256 = 0x00000007,
        K_283 = 0x00000008,
        B_283 = 0x00000009,
        P_384 = 0x0000000A,
        K_409 = 0x0000000B,
        B_409 = 0x0000000C,
        P_521 = 0x0000000D,
        K_571 = 0x0000000E,
        B_571 = 0x0000000F,
        Extensions = 80000000

    }
}
