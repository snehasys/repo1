using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TSUnion.KMIP.Communication
{
    public class TransportObjectHelper
    {

        public static int ConvertResponseMessageToByte(object message)
        {
            if (message == null)
                return 0;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, message);
            return ms.ToArray().Length;
        }
    }
}
