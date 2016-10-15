using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.CryptographicObjects;

namespace TSUnion.KMIP.DAO.MSSql
{
    public class LocalFileKMIPObjectManager : IKMIPObjectManager
    {
        private readonly string StoragePath = Directory.GetCurrentDirectory() + "\\Storage";
        private readonly string ArchivePath = Directory.GetCurrentDirectory() + "\\Archive";

        #region Члены IKMIPObjectManager

        public bool Archive(object obj)
        {
           return FileManager.Replace(StoragePath, ArchivePath, ((BaseObject)obj).Id);
        }

        public int DeleteAllObjects()
        {

            int filesCount = (new DirectoryInfo(StoragePath)).GetFiles().Count();
            Directory.Delete(StoragePath, true);
            return filesCount;
        }

        public Core.BaseObject GetBaseObject(Guid id)
        {
            return (BaseObject)GetObject(id);
        }

        public Core.BaseObject GetBaseObjectFromArchive(Guid id)
        {
            return (BaseObject)GetObjectFromArchive(id);
        }

        public object GetObject(Guid id)
        {
            FileKmipObject kmipKeyFile = FileManager.LoadObject(StoragePath, id);


            if (kmipKeyFile.Type == 0)
            {
                return null;
            }
            else
            {

                object obj;
                if (kmipKeyFile.Type == Certificate.STORAGE_TYPE)
                {
                    return GenericConvertationManager<Certificate>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;

                }

                if (kmipKeyFile.Type == PrivateKey.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<PrivateKey>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }

                if (kmipKeyFile.Type == PublicKey.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<PublicKey>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }

                if (kmipKeyFile.Type == SplitKey.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<SplitKey>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }

                if (kmipKeyFile.Type == SymmetricKey.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<SymmetricKey>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }
                if (kmipKeyFile.Type == SecretData.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<SecretData>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }

                if (kmipKeyFile.Type == BaseObject.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<BaseObject>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }


            }
            return null;

        }

        public object GetObjectFromArchive(Guid id)
        {
            FileKmipObject kmipKeyFile = FileManager.LoadObject(ArchivePath, id);


            if (kmipKeyFile.Type == 0)
            {
                return null;
            }
            else
            {

                object obj;
                if (kmipKeyFile.Type == Certificate.STORAGE_TYPE)
                {
                    return GenericConvertationManager<Certificate>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;

                }

                if (kmipKeyFile.Type == PrivateKey.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<PrivateKey>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }

                if (kmipKeyFile.Type == PublicKey.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<PublicKey>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }

                if (kmipKeyFile.Type == SplitKey.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<SplitKey>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }

                if (kmipKeyFile.Type == SymmetricKey.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<SymmetricKey>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }
                if (kmipKeyFile.Type == SecretData.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<SecretData>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }

                if (kmipKeyFile.Type == BaseObject.STORAGE_TYPE)
                {
                    obj = GenericConvertationManager<BaseObject>.ConvertByteArrayToObject(kmipKeyFile.Value.ToArray());
                    ((BaseObject)obj).Id = id;
                    return obj;
                }


            }
            return null;
        }

        public Guid InsertObject(object obj)
        {
            var kmipObject = new FileKmipObject();


            kmipObject.ID = Guid.NewGuid();
            kmipObject.Value = ConvertationManager.ConvertObjectToByteArray(obj);
            kmipObject.Type = Convert.ToInt32(obj.GetType().GetFields().Where(f => f.Name.Contains("STORAGE_TYPE")).Single().GetValue(obj));
            kmipObject.Created = DateTime.Now;
            kmipObject.Attributes = ((BaseObject)obj).GetAttributeList();
            FileManager.CreateObject(StoragePath, kmipObject.ID, kmipObject);
            return kmipObject.ID;
        }

        public bool IsArchived(Guid id)
        {
            return FileManager.IsArchived(ArchivePath, id);
        }

        public bool Recover(object obj)
        {
            return FileManager.Replace(ArchivePath, StoragePath, ((BaseObject)obj).Id);
        }

        public Guid UpdateObject(object obj)

        {
            Guid id = ((BaseObject)obj).Id;
            FileManager.DeleteObject(StoragePath, id);

            var kmipObject = new FileKmipObject();
            kmipObject.ID = ((BaseObject)obj).Id;
            kmipObject.Value = ConvertationManager.ConvertObjectToByteArray(obj);
            kmipObject.Type = Convert.ToInt32(obj.GetType().GetFields().Where(f => f.Name.Contains("STORAGE_TYPE")).Single().GetValue(obj));
            kmipObject.Created = DateTime.Now;
            kmipObject.Attributes = ((BaseObject)obj).GetAttributeList();

            FileManager.CreateObject(StoragePath, kmipObject.ID, kmipObject);
            return kmipObject.ID;
             
        }

        #endregion
    }
}
