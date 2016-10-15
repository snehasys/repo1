using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    internal enum NameType
    {
        UninterpretedTextString = 0x00000001,
        URI = 0x00000002,
        Extensions = 0x70000001,
    }
}
