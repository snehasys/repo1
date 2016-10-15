using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.KeyMaterials
{
    [Serializable]
    public class SymmetricKeyMaterial : KeyMaterial<SymmetricKeyMaterial>
    {
        public byte[] Key { get; set; }

        public override SymmetricKeyMaterial Instance
        {
            get
            {
                return this;
            }
        }

    }
}
