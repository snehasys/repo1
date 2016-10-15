using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Common;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Communication
{
    public class NativeMessageConverter : IMessageConverter
    {
        
        public byte[] ConvertResponseMessageToByte(ResponseMessage message)
        {

            foreach (object attr in message.Header.ProtocolVersion.GetType().GetCustomAttributes(true))
            {
                if (attr is EncodingAttribute)
                {
                    EncodingAttribute encodingAttrib = attr as EncodingAttribute;
                    Int32 tag = (Int32)encodingAttrib.Tag;
                    Int16 dataType = (Int16)encodingAttrib.Data;
                    Int32 lenght = (Int16)System.Runtime.InteropServices.Marshal.SizeOf(message.Header.ProtocolVersion);
                }
            }

            return new byte[0];
        }


        public byte[] ConvertRequestMessageToByte(RequestMessage message)
        {
            byte[] buffer = new byte[0];
            //-------------------------header----------------------
            PropertyInfo HeaderInfo = message.GetType().GetProperty("Header");
            int HeaderSize = message.Header.GetSize();
            EncodingAttribute HeaderInfoAttrib = ((EncodingAttribute)HeaderInfo.GetCustomAttributes(true).Single());
            buffer = GetTTLOfFragment(buffer, HeaderSize, HeaderInfoAttrib);



            //-------------------------ProtocolVersion----------------------
            PropertyInfo ProtocolVersionInfo = message.Header.GetType().GetProperty("ProtocolVersion");
            int ProtocolVersionSize = message.Header.ProtocolVersion.GetSize();
            EncodingAttribute ProtocolVersionAttrib = ((EncodingAttribute)ProtocolVersionInfo.GetCustomAttributes(true).Single());
            buffer = GetTTLOfFragment(buffer, ProtocolVersionSize, ProtocolVersionAttrib);


            //-------------------------ProtocolVersionMajor----------------------
            PropertyInfo ProtocolVersionMajorInfo = message.Header.ProtocolVersion.GetType().GetProperty("ProtocolVersionMajor");
            int ProtocolVersionMajorSize = 4;
            EncodingAttribute ProtocolVersionMajorAttrib = ((EncodingAttribute)ProtocolVersionMajorInfo.GetCustomAttributes(true).Single());
            buffer = GetTTLOfFragment(buffer, ProtocolVersionMajorSize, ProtocolVersionMajorAttrib);
            buffer = ArrayHelper.AddToArray(buffer, ArrayHelper.ConvertInt32ToByteArray(message.Header.ProtocolVersion.ProtocolVersionMajor));

            //-------------------------ProtocolVersionMinor----------------------
            PropertyInfo ProtocolVersionMinorInfo = message.Header.ProtocolVersion.GetType().GetProperty("ProtocolVersionMinor");
            int ProtocolVersionMinorSize = 4;
            EncodingAttribute ProtocolVersionMinorAttrib = ((EncodingAttribute)ProtocolVersionMinorInfo.GetCustomAttributes(true).Single());
            buffer = GetTTLOfFragment(buffer, ProtocolVersionMinorSize, ProtocolVersionMinorAttrib);
            buffer = ArrayHelper.AddToArray(buffer, ArrayHelper.ConvertInt32ToByteArray(message.Header.ProtocolVersion.ProtocolVersionMinor));

            PropertyInfo MaximumResponseSizeInfo = message.Header.GetType().GetProperty("MaximumResponseSize");
            PropertyInfo AsynchronousIndicatorInfo = message.Header.GetType().GetProperty("AsynchronousIndicator");
            PropertyInfo AuthenticationInfo = message.Header.GetType().GetProperty("Authentication");
            PropertyInfo BatchErrorContinuationOptionInfo = message.Header.GetType().GetProperty("BatchErrorContinuationOption");
            PropertyInfo BatchOrderOptionInfo = message.Header.GetType().GetProperty("BatchOrderOption");
            PropertyInfo TimeStampInfo = message.Header.GetType().GetProperty("TimeStamp");
            PropertyInfo BatchCountInfo = message.Header.GetType().GetProperty("BatchCount");


            foreach (object attr in HeaderInfo.GetCustomAttributes(true))
            {
                if (attr is EncodingAttribute)
                {
                    EncodingAttribute encodingAttrib = attr as EncodingAttribute;
                    Int32 tag = (Int32)encodingAttrib.Tag;
                    Int16 dataType = (Int16)encodingAttrib.Data;
                    Int32 lenght = (Int16)System.Runtime.InteropServices.Marshal.SizeOf(message.Header.ProtocolVersion);
                }


            }


            PropertyInfo BatchItemInfo = message.GetType().GetProperty("BatchItem");

            return new byte[0];
        }

        public static byte[] GetTTLOfFragment(byte[] buffer, int size, EncodingAttribute HeaderInfoAttrib)
        {
            buffer = ArrayHelper.AddToArray(buffer, ArrayHelper.TransformInt32to3ByteArray((int)HeaderInfoAttrib.Tag));
            buffer = ArrayHelper.AddToArray(buffer, ArrayHelper.TransformInt32toByte((int)HeaderInfoAttrib.Data));
            buffer = ArrayHelper.AddToArray(buffer, ArrayHelper.ConvertInt32ToByteArray(size));
            return buffer;
        }


        public RequestMessage ConvertByteArrayToRequestMessage(byte[] array)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage ConvertByteArrayToResponseMessage(byte[] array)
        {
            throw new NotImplementedException();
        }

        #region Члены IMessageConverter


        public NetworkMessage ConvertByteArrayToNetworkMessage(byte[] array)
        {
            throw new NotImplementedException();
        }

        public byte[] ConvertNetworkMessageToByte(NetworkMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
