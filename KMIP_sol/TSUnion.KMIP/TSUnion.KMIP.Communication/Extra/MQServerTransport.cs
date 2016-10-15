using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Apache.NMS;
using Apache.NMS.Util;
using log4net;
using TSUnion.KMIP.Communication.Transport;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Communication.Extra
{
    class MQServerTransport : IServerTransport
    {
        #region Члены IServerTransport

        private static ILog Log = LogManager.GetLogger(typeof(MQServerTransport));
        private static MQServerTransport instance;

        private IConnectionFactory factory;
        private Uri connecturi;
        private IConnection connection;
        private ISession session;
        private IDestination destination;

        private MQServerTransport()
        {

        }


        public void CreateServerTunnel(ConnectionDetails details)
        {
            connecturi = new Uri(details.MQConnectionDetails );
            factory = new NMSConnectionFactory(connecturi);
            connection = factory.CreateConnection();
            session = connection.CreateSession();
            destination = SessionUtil.GetDestination(session, details.MQQueueName);
            Log.Info("Communication channel started.");
            connection.Start();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ListenChannel));
        }

        private void ListenChannel(object state)
        {
            using (IMessageConsumer consumer = session.CreateConsumer(destination))
           
            {
                // Start the connection so that messages will be processed.
               
                // Consume a message
                while (true)
                {
                    Thread.Sleep(500);
                    IObjectMessage message = consumer.Receive() as IObjectMessage;
                    if (message != null)
                    {
                       
                        Log.Info("######################################################");
                        Log.Info("#                                                    #");
                        Log.Info("#                  New message income                #");
                        Log.Info("#                                                    #");
                        Log.Info("######################################################");
                        Log.Info("Received message with ID:   " + message.NMSMessageId);
                        

                        IMessageConverter communicationHelper = new SimpleMessageConverter();
                        NetworkMessage requestMessage = message.ToObject<NetworkMessage>();
                        Log.Info("Message decoded.");

                        if (ServerRequestReceived != null)
                        {
                            Log.Info("Sent data to handle.");
                            ServerRequestReceived(requestMessage.Request, requestMessage.Source);
                        }
                    }
                }
            }
        }

        public void RemoveServerTunnel()
        {
            if (session != null)
                session.Dispose();
            if (connection != null)
                connection.Dispose();
        }

        public event MessageReceivedEventHandler ServerRequestReceived;

        public void SendResponseToClient(ResponseMessage response, IPAddress ip)
        {
            
            using (IMessageProducer producer = session.CreateProducer(destination))
            {
                // Start the connection so that messages will be processed.
               
               

                // Send a message
                IObjectMessage request = session.CreateObjectMessage(response);
                request.NMSMessageId = Guid.NewGuid().ToString();
     
                producer.Send(request);

                
            }
        }

        #endregion

        internal static MQServerTransport Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MQServerTransport();
                    Log.Info("MQServerTransport singleton instance has been crated.");

                }
                return instance;
            }
        }
    }
}
