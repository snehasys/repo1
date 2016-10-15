using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Communication
{
    public class SimpleMessageConverter : IMessageConverter
    {
        #region Члены IMessageHelper

        public RequestMessage ConvertByteArrayToRequestMessage(byte[] array)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(array, 0, array.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            RequestMessage obj = (RequestMessage)binForm.Deserialize(memStream);
            return obj;
        }

        public Core.Response.ResponseMessage ConvertByteArrayToResponseMessage(byte[] array)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(array, 0, array.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            ResponseMessage obj = (ResponseMessage)binForm.Deserialize(memStream);
            return obj;
        }

        public byte[] ConvertRequestMessageToByte(Core.Request.RequestMessage message)
        {
            if (message == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, message);
            return ms.ToArray();
        }

        public byte[] ConvertResponseMessageToByte(Core.Response.ResponseMessage message)
        {
            if (message == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, message);
            return ms.ToArray();
        }

        #endregion

        #region Члены IMessageConverter


        public NetworkMessage ConvertByteArrayToNetworkMessage(byte[] array)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(array, 0, array.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            NetworkMessage obj = (NetworkMessage)binForm.Deserialize(memStream);
            return obj;
        }

        public byte[] ConvertNetworkMessageToByte(NetworkMessage message)
        {
            if (message == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, message);
            return ms.ToArray();
        }

        #endregion
    }
}
