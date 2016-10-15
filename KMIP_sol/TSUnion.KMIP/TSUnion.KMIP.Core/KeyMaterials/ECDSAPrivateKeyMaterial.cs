using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.KeyMaterials
{
    [Serializable]
    public class ECDSAPrivateKeyMaterial : KeyMaterial<ECDSAPrivateKeyMaterial>
    {
        public RecommendedCurveType CurveType { get; set; }
        public byte[] D { get; set; }
    }
}
