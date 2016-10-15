using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace TSUnion.KMIP.Core
{
    internal class DigectHelper
    {
        public static byte[] GetSHA256DigestOfObject(object obj) 
        {
            if (obj == null)
                return new byte[0];
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] hashedKey = sha.ComputeHash(ms.ToArray());
            return hashedKey;

        }
    }
}
