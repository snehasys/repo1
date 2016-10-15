using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core.KeyMaterials
{
    [Serializable]
    public class KeyMaterial<TType> where TType : class
    {
        public virtual TType Instance
        {
            get
            {
                var instance = default(TType);
                return instance;

            }
        }

        public static TType ConvertTo(object obj)
        {
            if (obj is TType)
            {
                return obj as TType;
            }
            else return null;
        }



    }
}
