using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.KeyMaterials
{
     [Serializable]
    public class RSAPublicKeyMaterial : KeyMaterial<RSAPublicKeyMaterial>
    {
        public byte[] Modulus { get; set; }
        public byte[] PublicExponent { get; set; }
    }
}
