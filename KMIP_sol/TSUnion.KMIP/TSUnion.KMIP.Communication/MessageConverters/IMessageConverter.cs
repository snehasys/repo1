using System;
namespace TSUnion.KMIP.Communication
{
    public interface IMessageConverter
    {
        TSUnion.KMIP.Core.Request.RequestMessage ConvertByteArrayToRequestMessage(byte[] array);
        NetworkMessage ConvertByteArrayToNetworkMessage(byte[] array);
        TSUnion.KMIP.Core.Response.ResponseMessage ConvertByteArrayToResponseMessage(byte[] array);
        byte[] ConvertRequestMessageToByte(TSUnion.KMIP.Core.Request.RequestMessage message);
        byte[] ConvertNetworkMessageToByte(NetworkMessage message);
        byte[] ConvertResponseMessageToByte(TSUnion.KMIP.Core.Response.ResponseMessage message);
    }
}
