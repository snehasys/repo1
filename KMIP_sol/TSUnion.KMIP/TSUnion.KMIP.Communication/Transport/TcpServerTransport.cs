using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;
using TSUnion.KMIP.Communication.Transport;
using System.Net;


namespace TSUnion.KMIP.Communication.Transport
{
    public delegate void MessageReceivedEventHandler(RequestMessage request, IPAddress address);

    public class TcpServerTransport : IServerTransport
    {
        private static TcpServerTransport instance;

        private ILog Log = LogManager.GetLogger(typeof(TcpServerTransport));
        private ConnectionDetails connDetails;

        private TcpListener server;
        private NetworkStream serverStream;
        public event MessageReceivedEventHandler ServerRequestReceived; 



        internal static TcpServerTransport Instance 
        {
            get 
            {
                if (instance == null) 
                {
                    instance = new TcpServerTransport();
                }
                return instance;
            }
        }

      

        public void CreateServerTunnel(ConnectionDetails details)
        {
            Log.Info("Trying to create communication channel using: " + this.ToString());
            connDetails = details;

            server = new TcpListener(details.IpAddress, details.Port);
            Log.Info("Address: " + details.IpAddress);
            Log.Info("Port: " + details.Port);
            server.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            server.ExclusiveAddressUse = false;
            
            server.Start();
            Log.Info("Server has been started.");
            Log.Info("Communication channel started.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ListenChannel));

           
        }

        private void ListenChannel(Object stateInfo) 
        {
            Byte[] buffer = new Byte[connDetails.BufferSize];
            // You could also user server.AcceptSocket() here.
            if (server != null)
            {

                while (true)
                {
                    try
                    {
                        TcpClient client = server.AcceptTcpClient();



                        // Get a stream object for reading and writing
                        serverStream = client.GetStream();

                        int i;


                        while ((i = serverStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            Log.Info("######################################################");
                            Log.Info("#                                                    #");
                            Log.Info("#                  New message income                #");
                            Log.Info("#                                                    #");
                            Log.Info("######################################################");
                            Log.Info("Received Request message with length (in bytes)" + buffer.Length);
                            IMessageConverter communicationHelper = new SimpleMessageConverter();
                            NetworkMessage message = communicationHelper.ConvertByteArrayToNetworkMessage(buffer);

                            Log.Info("Network Message decoded.");
                            Log.Info("Request Source IP address: " + message.Source.ToString());
                            Log.Info("Request Destiation IP address: " + message.Destination.ToString());
                            if (message.Request == null)
                            {
                                Log.Error("Request message is NULL exported from the Metwork message");
                                continue;
                            }
                            if (ServerRequestReceived != null)
                            {
                                Log.Info("Sent data to handle.");

                                ServerRequestReceived(message.Request, message.Source);
                            }



                        }

                    }
                    catch (Exception ex)
                    {
                        Log.Fatal(ex.Message);
                        if (ex.InnerException != null) 
                        {
                            Log.Fatal(ex.InnerException);
                        }
                    }

                }
            }
            else
            {
                Log.Error("Server is null...");
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
            serverStream.Write(responseMessage, 0, responseMessage.Length);
            Log.Info("Response message sent to Client.");
        }
    }
}
