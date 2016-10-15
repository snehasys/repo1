using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum QueryFunctionType
    {
        QueryOperations = 0x00000001,
        QueryObjects = 0x00000002,
        QueryServerInformation = 0x00000003,
        QueryApplicationNamespaces = 0x00000004,
        QueryExtensionList = 0x00000005,
        QueryExtensionMap = 0x00000006
    }
}
