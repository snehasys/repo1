using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace TSUnion.KMIP.Logger
{
    public class KmipLog
    {
        private static KmipLog instance;


        public static KmipLog Instance 
        {
            get 
            {
                if (instance == null) 
                {
                    instance = new KmipLog();
                }
                return instance;
            }
        }
        public ILog GetLogger (Type type)
        {
            return LogManager.GetLogger(type);   
        }

    }
}
