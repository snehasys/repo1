using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.CryptographicObjects
{
    [Serializable]
    public class PrivateKey : ManagedCryptographicObject
    {
        public const int STORAGE_TYPE = 0x00000002;
     
         public PrivateKey(ObjectType objType,DateTime expiredDate) : base( objType,expiredDate) { }
         public PrivateKey(ObjectType objType) : base( objType,DateTime.Now.AddMonths(1)) { }
    }
}
