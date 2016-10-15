using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TSUnion.KMIP.Communication.Transport;
using TSUnion.KMIP.Configuration;
using TSUnion.KMIP.Core;

namespace TSUnion.KMIP.Communication
{
    public class ConnectionDetails
    {
        
        Int32 port = 0;

        [AppSetting]
        public Int32 Port
        {
            get { return port; }
            set { port = value; }
        }
       private String localAddr;
       private ConverterType protocolType;

       [AppSetting]
       public ConverterType ProtocolType
       {
           get { return protocolType; }
           set { protocolType = value; }
       }
        [AppSetting]
        private String Address
        {
            get { return localAddr; }
            set {
               
                localAddr = value; 
            
            }
        }

        public IPAddress IpAddress 
        {
            get 
            {
                return IPAddress.Parse(localAddr);
            }
        }

        int bufferSize = 0;

        [AppSetting]
        public int BufferSize
        {
            get { return bufferSize; }
            set { bufferSize = value; }
        }


        private TrasportType transport;
        [AppSetting]
        public TrasportType Transport 
        {
            get { return transport; }
            set {  transport=value; }
        }

        public static ConnectionDetails CreateConnectionFromConfig()
        {
            ConnectionDetails details = new ConnectionDetails();
            return KmipConfigurationManager.ConfigureObject(details) as ConnectionDetails;
        }
        private String certPath;
        [AppSetting]
        public string CertificatePath 
        {
            get { return certPath; } 
            set { certPath = value; } 
        }


        private String certName;
        [AppSetting]
        public string CertificateName
        {
            get { return certName; }
            set { certName = value; }
        }

        private String mqConnectionDetails;
        [AppSetting]
        public string MQConnectionDetails
        {
            get
            {
                return mqConnectionDetails;
            }
            set
            {
                mqConnectionDetails = value;
            }
        }

        private string mqQueueName;
        public string MQQueueName
        {
            get { return mqQueueName; }
            set { mqQueueName = value; }
        }
    }
}
