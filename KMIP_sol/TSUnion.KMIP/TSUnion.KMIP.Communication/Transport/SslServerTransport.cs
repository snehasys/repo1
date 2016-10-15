using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using log4net;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Communication.Transport
{
    public class SslServerTransport:IServerTransport
    {
        #region Члены IServerTransport
        private static SslServerTransport instance;
        private static X509Certificate serverCertificate = null;
        private ILog Log = LogManager.GetLogger(typeof(SslServerTransport));
        
        
        
        private ConnectionDetails connDetails;
        private string certificatePath;
        private TcpListener server;
        private SslStream sslStream;
        public event MessageReceivedEventHandler ServerRequestReceived; 


        public void CreateServerTunnel(ConnectionDetails details)
        {
           
            // Create a TCP/IP (IPv4) socket and listen for incoming connections.
            Log.Info("Trying to create communication channel using: " + this.ToString());
            connDetails = details;

            
            server = new TcpListener(details.IpAddress, details.Port);
            certificatePath = details.CertificatePath;
           
            Log.Info("Address: " + details.IpAddress);
            Log.Info("Port: " + details.Port);
            Log.Info("Path to the server`s certificate is: " + certificatePath);

            server.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            server.ExclusiveAddressUse = false;
            server.Start();

            Log.Info("Communication channel started.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ListenChannel));

            serverCertificate = X509Certificate.CreateFromCertFile(certificatePath);
            if (serverCertificate != null)
            {
                Log.Info("X509 certificate has been created.");
                Log.Info("X509 details[Key Algorithm]:" + serverCertificate.GetKeyAlgorithm());
                Log.Info("X509 details[Name]:" + serverCertificate.GetName().ToString());
                Log.Info("X509 details[Subject]:" + serverCertificate.Subject);
                Log.Info("X509 details[Issuer]:" + serverCertificate.Issuer);
            }
            else 
            {
                Log.Fatal("Could not open SSL channel. Error during certificate creation.");
            }

            
        }

        private void ListenChannel(Object stateInfo)
        {
            Byte[] buffer = new Byte[connDetails.BufferSize];
            // You could also user server.AcceptSocket() here.
            if (server != null)
            {
                
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Log.Info("Client accepted...");
                    sslStream = new SslStream(client.GetStream(), false);
                    // Authenticate the server but don't require the client to authenticate.
                    try
                    {
                        sslStream.AuthenticateAsServer(serverCertificate,
                            false, SslProtocols.Tls, true);
                        Log.Info("Authenticate As Server completed");
                        // Display the properties and settings for the authenticated stream.
                        DisplaySecurityLevel(sslStream);
                        DisplaySecurityServices(sslStream);
                        DisplayCertificateInformation(sslStream);
                        DisplayStreamProperties(sslStream);

                        // Set timeouts for the read and write to 5 seconds.
                        sslStream.ReadTimeout = 5000;
                        sslStream.WriteTimeout = 5000;
                        // Read a message from the client.   

                        int i;


                        while ((i = sslStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            Log.Info("######################################################");
                            Log.Info("#                                                    #");
                            Log.Info("#                  New message income                #");
                            Log.Info("#                                                    #");
                            Log.Info("######################################################");
                            Log.Info("Received Request message with length (in bytes)" + buffer.Length);
                            IMessageConverter communicationHelper = new SimpleMessageConverter();
                            NetworkMessage message = communicationHelper.ConvertByteArrayToNetworkMessage(buffer);
                            Log.Info("Message decoded.");
                            Log.Info("Request Source IP address: " + message.Source.ToString());
                            Log.Info("Request Destiation IP address: " + message.Destination.ToString());
                       

                            if (ServerRequestReceived != null)
                            {
                                Log.Info("Sent data to handle.");
                                ServerRequestReceived(message.Request, message.Source);
                            }





                        }
                        
                    }
                    catch (AuthenticationException e)
                    {
                        Log.FatalFormat("Exception: {0}", e.Message);
                        if (e.InnerException != null)
                        {
                            Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                        }
                        Log.FatalFormat("Authentication failed - closing the connection.");
                        sslStream.Close();
                        client.Close();
                        return;
                    }
                    finally
                    {
                        // The client stream will be closed with the sslStream
                        // because we specified this behavior when creating
                        // the sslStream.
                        sslStream.Close();
                        client.Close();
                    }

                   

                    // Shutdown and end connection
                    client.Close();
                }
            }
            else
            {
                Log.Error("Server is null...");
            }
        }

        private void DisplaySecurityLevel(SslStream stream)
        {
            Log.InfoFormat("Cipher: {0} strength {1}", stream.CipherAlgorithm, stream.CipherStrength);
            Log.InfoFormat("Hash: {0} strength {1}", stream.HashAlgorithm, stream.HashStrength);
            Log.InfoFormat("Key exchange: {0} strength {1}", stream.KeyExchangeAlgorithm, stream.KeyExchangeStrength);
            Log.InfoFormat("Protocol: {0}", stream.SslProtocol);
        }
        private void DisplaySecurityServices(SslStream stream)
        {
            Log.InfoFormat("Is authenticated: {0} as server? {1}", stream.IsAuthenticated, stream.IsServer);
            Log.InfoFormat("IsSigned: {0}", stream.IsSigned);
            Log.InfoFormat("Is Encrypted: {0}", stream.IsEncrypted);
        }
        private void DisplayStreamProperties(SslStream stream)
        {
            Log.InfoFormat("Can read: {0}, write {1}", stream.CanRead, stream.CanWrite);
            Log.InfoFormat("Can timeout: {0}", stream.CanTimeout);
        }
        private void DisplayCertificateInformation(SslStream stream)
        {
            Log.InfoFormat("Certificate revocation list checked: {0}", stream.CheckCertRevocationStatus);

            X509Certificate localCertificate = stream.LocalCertificate;
            if (stream.LocalCertificate != null)
            {
                Log.InfoFormat("Local cert was issued to {0} and is valid from {1} until {2}.",
                    localCertificate.Subject,
                    localCertificate.GetEffectiveDateString(),
                    localCertificate.GetExpirationDateString());
            }
            else
            {
                Log.InfoFormat("Local certificate is null.");
            }
            
            X509Certificate remoteCertificate = stream.RemoteCertificate;
            if (stream.RemoteCertificate != null)
            {
                Log.InfoFormat("Remote cert was issued to {0} and is valid from {1} until {2}.",
                    remoteCertificate.Subject,
                    remoteCertificate.GetEffectiveDateString(),
                    remoteCertificate.GetExpirationDateString());
            }
            else
            {
                Log.InfoFormat("Remote certificate is null.");
            }
        }
      
        public void RemoveServerTunnel()
        {
            server.Stop();
        }

        

        public void SendResponseToClient(ResponseMessage response, IPAddress ip)
        {
            IMessageConverter communicationHelper = new SimpleMessageConverter();
            NetworkMessage netMessage = new NetworkMessage();
            netMessage.Destination = ip;
            netMessage.Response = response;
            netMessage.Source = Dns.GetHostAddresses(Dns.GetHostName()).First();

            byte[] responseMessage = communicationHelper.ConvertNetworkMessageToByte(netMessage);
            sslStream.Write(responseMessage, 0, responseMessage.Length);
            Log.Info("Response message sent to Client.");
        }

        #endregion

        public static IServerTransport Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SslServerTransport();
                }
                return instance;
            }
        }
    }
}
