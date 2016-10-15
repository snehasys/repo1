using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum BatchErrorContinuationOptionType
    {
        Continue = 00000001,
        Stop = 00000002,
        Undo = 00000003,
        Extensions = 81111111,

    }
}
