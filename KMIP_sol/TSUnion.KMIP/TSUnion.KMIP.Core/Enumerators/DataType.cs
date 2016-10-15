using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum DataType
    {
        Structure = 0x00000001,
        Integer = 0x00000002,
        LongInteger = 0x00000003,
        BigInteger = 0x00000004,
        Enumeration = 0x00000005,
        Boolean = 0x00000006,
        TextString = 0x00000007,
        ByteString = 0x00000008,
        DateTime = 0x00000009,
        Interval = 0x0000000A

    }
}
