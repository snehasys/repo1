using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Configuration;

namespace TSUnion.KMIP.Server.Base
{
    public class ServerSpecification
    {
        [AppSetting]
        public Int32 ProtocolVersionMajor { get; set; } 

        internal static ServerSpecification GetConfigSpecification()
        {
            ServerSpecification specification = new ServerSpecification();
            specification = (ServerSpecification)KmipConfigurationManager.ConfigureObject(specification);
            return specification;
           
        }
    }
}
