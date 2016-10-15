using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.Enumerators
{
    public enum ObjectType
    {
        Certificate = 0x00000001,
        SymmetricKey = 0x00000002,
        PublicKey = 0x00000003,
        PrivateKey = 0x00000004,
        SplitKey = 0x00000005,
        Template = 0x00000006,
        SecretData = 0x00000007,
        OpaqueObject = 0x00000008,
        Extensions = 0x70000001
    }
}
