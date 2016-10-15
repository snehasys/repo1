using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.CryptographicObjects
{
    [Serializable]
    public class SymmetricKey : ManagedCryptographicObject
    {
        public const int STORAGE_TYPE = 0x00000005;

        public SymmetricKey(ObjectType objType, DateTime expiredDate) : base(objType,expiredDate) { }
        public SymmetricKey(ObjectType objType) : base(objType,DateTime.Now.AddMonths(1)) { }
     
    }
}
