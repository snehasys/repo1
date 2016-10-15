using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core
{
    public class SecretData :BaseObject
    {
        public const int STORAGE_TYPE = 0x00000006;
        SecretDataType Type { get; set; }

        public SecretData(ObjectType objType) : base(objType) { }
       
    }
}
