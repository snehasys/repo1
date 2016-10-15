using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.CryptographicObjects
{
    [Serializable]
    public class PublicKey : ManagedCryptographicObject
    {
        public const int STORAGE_TYPE = 0x00000003;
        public PublicKey(ObjectType objType, DateTime expiredDate) : base(objType,expiredDate) { }
          public PublicKey(ObjectType objType) : base( objType,DateTime.Now.AddMonths(1)) { }
       
    }
}
