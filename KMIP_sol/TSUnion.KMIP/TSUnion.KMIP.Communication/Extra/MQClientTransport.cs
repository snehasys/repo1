using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Apache.NMS;
using Apache.NMS.Util;
using log4net;
using TSUnion.KMIP.Communication.Transport;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.Request;
using TSUnion.KMIP.Core.Response;

namespace TSUnion.KMIP.Communication.Extra
{
    class MQClientTransport : IClientTransport
    {


        private static ILog Log = LogManager.GetLogger(typeof(MQClientTransport));
        private static MQClientTransport instance;

        private IConnectionFactory factory;
        private Uri connecturi;
        private IConnection connection;
        private ISession session;
        private IDestination destination;
        private ConnectionDetails details;

        public void Connect(ConnectionDetails details, bool delay)
        {
            this.connecturi = new Uri(details.MQConnectionDetails);
            this.factory = new NMSConnectionFactory(connecturi);
            this.connection = factory.CreateConnection();
            this.session = connection.CreateSession();
            this.destination = SessionUtil.GetDestination(session, details.MQQueueName);
            this.details = details;
            this.connection.Start();

        }

        public void Connect(ConnectionDetails details)
        {
            Connect(details, false);
        }

        public void Close()
        {
            if (session != null)
                session.Dispose();
            if (connection != null)
                connection.Dispose();
        }

        public Guid SendRequest(RequestMessage message, ConverterType converterType)
        {
            NetworkMessage netMessage = new NetworkMessage();
            netMessage.Request = message;
            netMessage.Destination = details.IpAddress;
            netMessage.Source = Dns.GetHostAddresses(Dns.GetHostName()).First();

            using (IMessageProducer producer = session.CreateProducer(destination))
            {
                // Start the connection so that messages will be processed.



                // Send a message
                IObjectMessage request = session.CreateObjectMessage(netMessage);
                request.NMSMessageId = netMessage.ID.ToString();
                producer.Send(request);
            }
            return netMessage.ID;
        }

        public Guid SendRequest(RequestMessage message)
        {
          return  SendRequest(message, ConverterType.MQ);
        }

        public Core.Response.ResponseMessage WaitForResponse(ConverterType converterType)
        {
            IMessageConsumer consumer = session.CreateConsumer(destination);
            IObjectMessage message = consumer.Receive() as IObjectMessage;
            return message.ToObject<ResponseMessage>();
        }

        public Core.Response.ResponseMessage WaitForResponse()
        {
            return WaitForResponse(ConverterType.MQ);
        }



        public static IClientTransport Instance
        {
            get
            {
                if (instance == null)
                    instance = new MQClientTransport();
                return instance;
            }
        }
    }
}
