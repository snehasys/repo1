using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using log4net;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Communication.Transport
{
   public class SslClientTransport :IClientTransport
    {
        private static ILog Log = LogManager.GetLogger(typeof(SslClientTransport));

        private TcpClient client;
        private ConnectionDetails details;
        private SslStream stream;


        private SslClientTransport() { }
        private static SslClientTransport instance;
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
                stream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null
                );
                Log.Info("Channel created.");
                stream.AuthenticateAsClient(details.CertificateName);
                Log.Info("Auth passed.");
                Log.Info("Connected to the server.");
                
                
            }
            catch (AuthenticationException e)
            {
                Log.FatalFormat("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Log.FatalFormat("Inner exception: {0}", e.InnerException.Message);
                }
                Log.FatalFormat("Authentication failed - closing the connection.");
                client.Close();
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                Log.Fatal(e.Message);
                throw new Exception(e.Message);
            }
        }

        private bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Log.FatalFormat("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

        public Guid SendRequest(RequestMessage message)
        {
          return  SendRequest(message, details.ProtocolType);
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

        internal static SslClientTransport Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SslClientTransport();
                    Log.Info("SslClientTransport singleton instance has been crated."); 

                }
                return instance;
            }
        }

        #region Члены IClientTransport


        public void Connect(ConnectionDetails details)
        {
            Connect(details, false);
        }

        #endregion
    }
}
