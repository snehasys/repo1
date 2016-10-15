using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.KeyMaterials
{
    [Serializable]
    class DHPrivateKeyMaterial:KeyMaterial<DHPrivateKeyMaterial>
    {
        public byte[] P { get; set; }
        public byte[] Q { get; set; }
        public byte[] G { get; set; }
        public byte[] J { get; set; }
        public byte[] X { get; set; }
    }
}
