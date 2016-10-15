using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using log4net;
using TSUnion.KMIP.Core.Request;

namespace TSUnion.KMIP.Server.Base
{
    public class ServerTask
    {
        public Guid ID { get; set; }
        public RequestMessage Request { get; private set; }
        public IPAddress IP { get; private set; }


        private static ILog Log = LogManager.GetLogger(typeof(ServerTask));

        public ServerTask(RequestMessage message, IPAddress address)
        {
            ID = Guid.NewGuid();
            Log.Debug("[ServerTask contstructor]Server Task has been created with ID: " + ID.ToString());
            Request = message;
            Log.Debug("[ServerTask contstructor]Reuqest message size: " + Request.ToString());
            IP = address;
            Log.Debug("[ServerTask contstructor]Request Source IP Address: " + IP);
           

        }

        public ServerTask()
        {
            ID = Guid.Empty;

        }


        internal static ServerTask Default()
        {
            return new ServerTask();
        }
    }
}
