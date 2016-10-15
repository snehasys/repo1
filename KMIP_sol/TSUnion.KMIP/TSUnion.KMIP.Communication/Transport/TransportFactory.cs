using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using TSUnion.KMIP.Communication.Extra;

namespace TSUnion.KMIP.Communication.Transport
{
    public class TransportFactory
    {
        private static ILog Log = LogManager.GetLogger(typeof(TransportFactory));
        public static IClientTransport CreateClientChannel(TrasportType type) 
        {
            if (type == TrasportType.TCP) 
            {
                Log.Info(type.ToString() + " type of channel was choosen.");
                return TcpClientTransport.Instance;
            }

            if (type == TrasportType.SSL)
            {
                Log.Info(type.ToString() + " type of channel was choosen.");
                return SslClientTransport.Instance;
            }

            if (type == TrasportType.MQ)
            {
                Log.Info(type.ToString() + " type of channel was choosen.");
                return MQClientTransport.Instance;
            }
            return null;
        }


        public static IServerTransport GetServerChannel(TrasportType type)
        {
            if (type == TrasportType.TCP)
            {
                Log.Info(type.ToString() + " type of channel was choosen.");
                return TcpServerTransport.Instance;
            }

            if (type == TrasportType.SSL)
            {
                Log.Info(type.ToString() + " type of channel was choosen.");
                return SslServerTransport.Instance;
            }
            if (type == TrasportType.MQ)
            {
                Log.Info(type.ToString() + " type of channel was choosen.");
                return MQServerTransport.Instance;
            }
            return null;
        }
    }
}
