using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace TSUnion.KMIP.DAO.MSSql
{
    public class SheepDogWatcher
    {
        private static ILog Log = LogManager.GetLogger(typeof(SheepDogWatcher));

        public static IKMIPObjectManager GetStorage
        {
            get
            {
                if (MSSqlKMIPObjectManager.IsAvailable)
                {
                    Log.Info("MSSqlKMIPObjectManager is used for storage");
                    return new MSSqlKMIPObjectManager();
                }
                else
                {
                    Log.Info("LocalFileKMIPObjectManager is used for storage");
                    return new LocalFileKMIPObjectManager();
                }
            }
        }
    }
}
