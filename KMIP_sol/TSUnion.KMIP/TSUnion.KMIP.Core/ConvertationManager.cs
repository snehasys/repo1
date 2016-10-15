using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Core
{
    public class ConvertationManager
    {
        public static object ConvertByteArrayToObject(byte[] array)
        {
           return GenericConvertationManager<object>.ConvertByteArrayToObject(array);
        }


        public static byte[] ConvertObjectToByteArray(object obj)
        {
            return GenericConvertationManager<object>.ConvertObjectToByteArray(obj);
        }



    }
}
