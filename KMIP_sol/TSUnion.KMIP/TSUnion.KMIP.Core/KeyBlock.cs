using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core.Enumerators;
using TSUnion.KMIP.Core.KeyMaterials;

namespace TSUnion.KMIP.Core
{
    [Serializable]
    public class KeyBlock<TKeyType> : KeyBlockBase where TKeyType : class  
    {
      

        public KeyMaterial<TKeyType> Value { get; set; }
       
    }
}
