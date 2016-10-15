using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class ManagedCryptographicObject : BaseObject
    {

        public ManagedCryptographicObject(ObjectType objType) : base(objType, DateTime.Now.AddMonths(1), DateTime.Now.AddMonths(1)) { }
        public ManagedCryptographicObject(ObjectType objType, DateTime expiredDate) : base(objType,expiredDate, expiredDate) { }
        public ManagedCryptographicObject(ObjectType objType, DateTime expiredDate, DateTime stopProtectDate) : base(objType,expiredDate, stopProtectDate) { }
    }
}
