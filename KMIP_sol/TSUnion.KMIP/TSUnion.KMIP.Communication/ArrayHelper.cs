using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.Communication
{
    public  class ArrayHelper
    {
        public static byte[] ConvertInt32ToByteArray(Int32 intSize)
        {
            byte[] bytes = new byte[4];

            bytes[0] = (byte)(intSize >> 24);
            bytes[1] = (byte)(intSize >> 16);
            bytes[2] = (byte)(intSize >> 8);
            bytes[3] = (byte)intSize;


            return bytes;
        }

        public static byte[] AddToArray(byte[] source, byte[] addition) 
        {
            byte[] temp = new byte[source.Count() + addition.Count()];
            for (int i = 0; i < source.Count(); i++) 
            {
                temp[i] = source[i];
            }

            for (int i = 0; i < addition.Count(); i++)
            {
                temp[i+source.Count()] = addition[i];
            }
            return temp;

        
        }

        public static byte[] AddToArray(byte[] source, byte addition)
        {


            return AddToArray(source, new byte[] { addition });


        }

        public static byte[] TransformInt32to3ByteArray(Int32 size)
        {
            return ConvertInt32ToByteArray(size).Skip(1).ToArray();
        }

        public static byte[] TransformInt32toByte(Int32 size)
        {
            return ConvertInt32ToByteArray(size).Skip(3).ToArray();
        }
        
    }
}
