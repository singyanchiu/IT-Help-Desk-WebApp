using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLibraryNew2.DataAccess
{
    public class SqlDataAccess
    {
        public static string GetConnectString (string connectionName="DefaultConnection")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T> (string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static T LoadDataSingle<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectString()))
            {
                return cnn.QueryFirstOrDefault<T>(sql);
            }
        }

        public static int SaveData<T> (string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static int SaveDataGetId<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectString()))
            {
                return cnn.QuerySingle<int>(sql, data);
            }
        }


    public static T Query<T>(string sql, string connectionName)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectString(connectionName)))
            {
                return cnn.QueryFirstOrDefault<T>(sql);
            }
        }
    }
}
