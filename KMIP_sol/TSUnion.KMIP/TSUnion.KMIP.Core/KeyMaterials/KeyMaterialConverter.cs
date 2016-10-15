using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.KeyMaterials
{
    public class KeyMaterialConverter <TType> where TType:class
    {
        public static TType ConvertFrom(object obj)
        {
            if (obj is TType)
            {
                return obj as TType;
            }
            else return null;
        }
        


    }
}
