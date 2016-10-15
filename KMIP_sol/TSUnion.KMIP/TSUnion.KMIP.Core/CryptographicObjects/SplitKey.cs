using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;

namespace TSUnion.KMIP.Core.CryptographicObjects
{
    public class SplitKey : ManagedCryptographicObject
    {
        public const int STORAGE_TYPE = 0x00000004;
       
          public SplitKey(ObjectType objType,DateTime expiredDate) : base( objType,expiredDate) { }
          public SplitKey(ObjectType objType) : base( objType,DateTime.Now.AddMonths(1)) { }

        Int32 SplitKeyParts { get; set; }
        Int32 KeyPartIdentifierInteger { get; set; }
        Int32 SplitKeyThresholdInteger { get; set; }
        SplitKeyMethodType SplitKeyMethod { get; set; }
        UInt64 PrimeFieldSize { get; set; }
    }
}
