using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TSUnion.KMIP.DAO;

namespace TSUnion.KMIP.DAO
{
    public class DBHelper
    {
        public static bool IsServerConnected()
        {
            DAODataContext _dataConext=new DAODataContext();
            string connectionString = _dataConext.Connection.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException e)
                {
                    return false;
                }
                finally
                {
                    // not really necessary
                    connection.Close();
                }
            }
        }

        public static void DisableCaching()
        {
            DAODataContext _dataConext = new DAODataContext();
            _dataConext.ObjectTrackingEnabled = false;

        }
        public static string GetConnectionString 
        {
            get
            {
                DAODataContext _dataConext = new DAODataContext();
                return _dataConext.Connection.ConnectionString;
            }
        }
    }
}
