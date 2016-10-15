using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum StorageStatusMaskType
    {
        OnlineStorage = 00000001,
        ArchivalStorage = 00000002,
        Extensions = 11111110

    }
}
