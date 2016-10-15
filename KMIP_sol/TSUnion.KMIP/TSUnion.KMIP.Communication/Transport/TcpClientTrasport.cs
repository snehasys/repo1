using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;
using TSUnion.KMIP.Communication;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Communication.Transport
{
    public class TcpClientTransport : IClientTransport
    {
        private static ILog Log = LogManager.GetLogger(typeof(TcpClientTransport));


        private TcpClient client;
        private ConnectionDetails details;
        private NetworkStream stream;


        private TcpClientTransport() { }
        private static TcpClientTransport instance;

        internal static TcpClientTransport Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TcpClientTransport();
                    Log.Info("TcpClientTransport singleton instance has been crated."); 

                }
                return instance;
            }
        }



        public void Close()
        {
            if (stream != null) 
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
            }
        }




        public Guid SendRequest(RequestMessage message, ConverterType converter) 
        {
            NetworkMessage netMessage = new NetworkMessage();
            netMessage.Request = message;
            netMessage.Destination = details.IpAddress;
            netMessage.Source = Dns.GetHostAddresses(Dns.GetHostName()).First();

            byte[] data = MessageManager.GetInstance().GetMessageHelper(converter).ConvertNetworkMessageToByte(netMessage);

            stream.Write(data, 0, data.Length);
            return netMessage.ID;
        }



        public ResponseMessage WaitForResponse()
        {

            var responseBuffer = new Byte[details.BufferSize];
            Int32 bytes = stream.Read(responseBuffer, 0, responseBuffer.Length);
            NetworkMessage netMessage = MessageManager.GetInstance().GetMessageHelper(details.ProtocolType).ConvertByteArrayToNetworkMessage(responseBuffer);
            if (netMessage.Response != null)
            {
                return netMessage.Response;
            }
            return null;
        }


        public void Connect(ConnectionDetails details, bool delay)
        {
            Log.Info("Connecting to the server...");

            this.details = details;
            IPEndPoint endPoint = new IPEndPoint(details.IpAddress, details.Port);


            client = new TcpClient();
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;
            
            if (delay)
            {
                Thread.Sleep(2000);
            }
            try
            {
                client.Connect(endPoint);
                stream = client.GetStream();
            }
            catch (Exception e)
            {
                Log.Fatal(e.Message);
                throw new Exception(e.Message);
            }

           
        }



        #region Члены IClientTransport


        public Guid SendRequest(RequestMessage request)
        {
           return SendRequest(request, details.ProtocolType);
        }

        #endregion

        #region Члены IClientTransport


        public void Connect(ConnectionDetails details)
        {
            Connect(details, false);
        }

        #endregion

        #region Члены IClientTransport



        #endregion
    }
}
