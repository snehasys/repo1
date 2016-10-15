using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using log4net;

namespace TSUnion.KMIP.DAO.MSSql
{
    internal class FileManager
    {
        private static ILog Log = LogManager.GetLogger(typeof(FileManager));
        public static void CreateObject(string folder,Guid id, object obj) 
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                Log.Info("Storage " + folder + " does not exist. Created.");
            }
            else 
            {
                Log.Info("Storage " + folder + "  exist.");
            }

            using (FileStream fs = new FileStream(folder+"\\"+id.ToString()+".key" , FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Log.Info("Creating key on local drive with name:" + id.ToString() + ".key");
                bf.Serialize(fs, obj);
                if (File.Exists(folder + "\\" + id.ToString() + ".key"))
                {
                    Log.Info("Key created on local drive ");
                }
                else 
                {
                    Log.Error("Key was not created on local drive ");
                }
                 
            }
        }

        public static void DeleteObject(string folder, Guid id)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                Log.Info("Storage " + folder + " does not exist. Created.");
            }
            else
            {
                Log.Info("Storage " + folder + "  exist.");
            }

            if (File.Exists(folder + "\\" + id.ToString() + ".key")) 
            {
                File.Delete(folder + "\\" + id.ToString() + ".key");
            }
        }

        public static FileKmipObject LoadObject(string folder, Guid id)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                Log.Info("Storage " + folder + " does not exist. Created.");
                return null;
            }
            else
            {
                Log.Info("Storage " + folder + "  exist.");
            }

            using (FileStream fs = new FileStream(folder + "\\" + id.ToString() + ".key", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
             
                fs.Seek(0, SeekOrigin.Begin);
                FileKmipObject obj = (FileKmipObject)bf.Deserialize(fs);
                return obj;
            }

        }

        public static bool IsArchived(string folder, Guid id)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                Log.Info("Storage " + folder + " does not exist. Created.");
                return false;
            }


            return File.Exists(folder + "\\" + id.ToString() + ".key");
        }


        public static bool Replace(string sourceFolder, string destFolder, Guid id)
        {
            if (!Directory.Exists(sourceFolder))
            {
                Directory.CreateDirectory(sourceFolder);
                Log.Info("Archive folder" + sourceFolder + " does not exist. Created.");
              
            }

            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
                Log.Info("Storage folder" + sourceFolder + " does not exist. Created.");

            }

            string sourceFileName = sourceFolder + "\\" + id.ToString() + ".key";
            string destFileName = destFolder + "\\" + id.ToString() + ".key";
            File.Move(sourceFileName, destFileName);
            File.Delete(sourceFileName);
            return true;
        }


    }

}
