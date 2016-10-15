using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.KeyMaterials
{
    [Serializable]
    public class RSAPrivateKeyMaterial : KeyMaterial<RSAPrivateKeyMaterial>
    {
        public long Modulus { get; set; }
        public long PrivateExponent { get; set; }
        public long PublicExponent { get; set; }
        public byte[] P { get; set; }
        public byte[] Q { get; set; }
        public long PrimeExponentP { get; set; }
        public long PrimeExponentQ { get; set; }
        public long CRT { get; set; }
    }
}
