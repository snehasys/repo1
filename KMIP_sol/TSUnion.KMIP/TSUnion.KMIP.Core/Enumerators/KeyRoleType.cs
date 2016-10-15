using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum KeyRoleType
    {
        BDK = 0x00000001,
        CVK = 0x00000002,
        DEK = 0x00000003,
        MKAC = 0x00000004,
        MKSMC = 0x00000005,
        MKSMI = 0x00000006,
        MKDAC = 0x00000007,
        MKDN = 0x00000008,
        MKCP = 0x00000009,
        MKOTH = 0x0000000A,
        KEK = 0x0000000B,
        MAC16609 = 0x0000000C,
        MAC97971 = 0x0000000D,
        MAC97972 = 0x0000000E,
        MAC97973 = 0x0000000F,
        MAC97974 = 0x00000010,
        MAC97975 = 0x00000011,
        ZPK = 0x00000012,
        PVKIBM = 0x00000013,
        PVKPVV = 0x00000014,
        PVKOTH = 0x00000015

    }
}
