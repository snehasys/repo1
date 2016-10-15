using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum SplitKeyMethodType
    {
        XOR = 0x00000001,
        PolynomialSharingGF_216 = 0x00000002,
        PolynomialSharingPrimeField = 0x00000003,
        Extensions = 0x20000001
    }
}
