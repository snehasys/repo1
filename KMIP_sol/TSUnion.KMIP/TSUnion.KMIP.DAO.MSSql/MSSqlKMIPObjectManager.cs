using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core;
using TSUnion.KMIP.Core.CryptographicObjects;
using TSUnion.KMIP.DAO;

namespace TSUnion.KMIP.DAO
{
    public class MSSqlKMIPObjectManager : IKMIPObjectManager
    {
        private static DAODataContext dataContext = new DAODataContext();

        public static bool IsAvailable 
        {
            get 
            {
                string connectionString = dataContext.Connection.ConnectionString.Replace("Integrated Security=True", "Integrated Security=SSPI");
                //connectionString = "data source=SNEHASYS-II\\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true";
                //connectionString= ConfigurationManager.ConnectionStrings[0].ConnectionString.Replace(@".\",@"SNEHASYS-II\");
                using (var l_oConnection = new SqlConnection(connectionString))// ConfigurationManager.ConnectionStrings[0].ConnectionString))
                {
                    try
                    {
                        

                        l_oConnection.Open();
                        
                        l_oConnection.Close();
                        return true;
                    }
                    catch (SqlException E)
                    {
                        Console.WriteLine("*************\nSQL Exception has occured " +E.ToString());
                        return false;
                    }
                }
            }
        }
        public Guid  InsertObject(object obj)
        {
            var kmipObject = new DAO.KMIP_OBJECT();
      

            kmipObject.ID = Guid.NewGuid();
            kmipObject.Value = ConvertationManager.ConvertObjectToByteArray(obj);
            kmipObject.Type = Convert.ToInt32(obj.GetType().GetFields().Where(f => f.Name.Contains("STORAGE_TYPE")).Single().GetValue(obj));
            kmipObject.Created = DateTime.Now;
            kmipObject.Attributes = ((BaseObject)obj).GetAttributeList();

            dataContext.KMIP_OBJECT.InsertOnSubmit(kmipObject);
            dataContext.SubmitChanges();

            return kmipObject.ID;

        }

        public Guid UpdateObject(object obj)
        {

            var query = from n in dataContext.KMIP_OBJECT
                        where n.ID == (obj as BaseObject).Id
                        select n;
             
            if (query.Count() == 0)
            {
                throw new NullReferenceException("DAO object cound not find Managed Object in the DB with ID: " + (obj as BaseObject).Id.ToString());
            }


            KMIP_OBJECT kmipObject = query.Single();
            kmipObject.Value = ConvertationManager.ConvertObjectToByteArray(obj);
            kmipObject.Updated = DateTime.Now;

            kmipObject.Attributes = ((BaseObject)obj).GetAttributeList();
            dataContext.SubmitChanges();
            dataContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, kmipObject);
            return kmipObject.ID;

        }




        public object GetObject(Guid id)
        {
            DAODataContext tempDataContext = new DAODataContext();
            var query = from n in tempDataContext.KMIP_OBJECT
                        where n.ID == id
                        select n;

            if (query.Count() == 0)
            {
                return null;
            }
            else
            {
                if (query.Single().Type == 0)
                {
                    return null;
                }
                else
                {
                    object obj = null;
                   
                    if (query.Single().Type == Certificate.STORAGE_TYPE)
                    {
                        obj= GenericConvertationManager<Certificate>.ConvertByteArrayToObject( query.Single().Value.ToArray());
                     
                    }

                    if (query.Single().Type == PrivateKey.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<PrivateKey>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }

                    if (query.Single().Type == PublicKey.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<PublicKey>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }

                    if (query.Single().Type == SplitKey.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<SplitKey>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }

                    if (query.Single().Type == SymmetricKey.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<SymmetricKey>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }
                    if (query.Single().Type == SecretData.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<SecretData>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }

                    if (query.Single().Type == BaseObject.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<BaseObject>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }
                    ((BaseObject)obj).Id = id;
                    return obj;
                }
            }

            
        }

        public BaseObject GetBaseObject(Guid id)
        {
            return GetObject(id) as BaseObject;
        }


        public bool Archive(object obj) 
        {
            var query = from n in dataContext.KMIP_OBJECT
                        where n.ID == (obj as BaseObject).Id
                        select n;

            if (query.Count() == 0)
            {
                throw new NullReferenceException("DAO object cound not find Managed Object in the DB with ID: " + (obj as BaseObject).Id.ToString());
            }


            KMIP_OBJECT kmipObject = query.Single();

            KMIP_OBJECT_ARCHIVE kmipObjectArchived = new KMIP_OBJECT_ARCHIVE();
            kmipObjectArchived.ID = kmipObject.ID;
            kmipObjectArchived.Value = kmipObject.Value;
            kmipObjectArchived.Archived = DateTime.Now;
            kmipObjectArchived.Type = kmipObject.Type;


            dataContext.KMIP_OBJECT_ARCHIVE.InsertOnSubmit(kmipObjectArchived);
            dataContext.KMIP_OBJECT.DeleteOnSubmit(kmipObject);

            dataContext.SubmitChanges();
            return true;
          
        }


        public bool Recover(object obj)
        {
            var query = from n in dataContext.KMIP_OBJECT_ARCHIVE
                        where n.ID == (obj as BaseObject).Id
                        select n;

            if (query.Count() == 0)
            {
                throw new NullReferenceException("DAO object cound not find Managed Object in the DB with ID: " + (obj as BaseObject).Id.ToString());
            }


            KMIP_OBJECT_ARCHIVE kmipObjectArchived = query.Single();

            KMIP_OBJECT kmipObject = new KMIP_OBJECT();
            kmipObject.ID = kmipObjectArchived.ID;
            kmipObject.Value = kmipObjectArchived.Value;
            kmipObject.Type = kmipObjectArchived.Type;
            


            dataContext.KMIP_OBJECT_ARCHIVE.DeleteOnSubmit(kmipObjectArchived);
            dataContext.KMIP_OBJECT.InsertOnSubmit(kmipObject);

            dataContext.SubmitChanges();
            return true;
        }

        public BaseObject GetBaseObjectFromArchive(Guid id)
        {
            return GetObjectFromArchive(id) as BaseObject;
        }

        public object GetObjectFromArchive(Guid id)
        {
            DAODataContext tempDataContext = new DAODataContext();
            var query = from n in tempDataContext.KMIP_OBJECT_ARCHIVE
                        where n.ID == id
                        select n;

            if (query.Count() == 0)
            {
                return null;
            }
            else
            {
                if (query.Single().Type == 0)
                {
                    return null;
                }
                else
                {
                    object obj = null;

                    if (query.Single().Type == Certificate.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<Certificate>.ConvertByteArrayToObject(query.Single().Value.ToArray());

                    }

                    if (query.Single().Type == PrivateKey.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<PrivateKey>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }

                    if (query.Single().Type == PublicKey.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<PublicKey>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }

                    if (query.Single().Type == SplitKey.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<SplitKey>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }

                    if (query.Single().Type == SymmetricKey.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<SymmetricKey>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }
                    if (query.Single().Type == SecretData.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<SecretData>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }

                    if (query.Single().Type == BaseObject.STORAGE_TYPE)
                    {
                        obj = GenericConvertationManager<BaseObject>.ConvertByteArrayToObject(query.Single().Value.ToArray());
                    }
                    ((BaseObject)obj).Id = id;
                    return obj;
                }
            }


        }

        public int DeleteAllObjects()
        {
          
            int removedAmount = dataContext.KMIP_OBJECT.Count();
            dataContext.KMIP_OBJECT.DeleteAllOnSubmit(dataContext.KMIP_OBJECT.AsEnumerable());
            dataContext.SubmitChanges();
            return removedAmount;

        }

        public bool IsArchived(Guid guid)
        {
            DAODataContext tempDataContext = new DAODataContext();
            var query = from n in tempDataContext.KMIP_OBJECT_ARCHIVE
                        where n.ID == guid
                        select n;
            return query.Count() == 0 ? false : true;
        }
    }
}
