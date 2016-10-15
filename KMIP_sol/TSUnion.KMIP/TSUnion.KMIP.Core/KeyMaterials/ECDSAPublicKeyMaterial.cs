using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.KeyMaterials
{
    [Serializable]
    public class ECDSAPublicKeyMaterial:KeyMaterial<ECDSAPublicKeyMaterial>
    {
        public RecommendedCurveType CurveType { get; set; }
        public byte[] Q { get; set; }
    }
}
