using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataTier.Dao
{
    public class DaoLib
    {
        public static string ConnectionString =
                @"Data Source=thientran-sv\SQLEXPRESS;Initial Catalog=TheProject;Integrated Security=SSPI;User ID=sa;Password=Thien12345;";
//        public static string ConnectionString =
//                @"Data Source=DESKTOP-UA7DUJ6\SQLEXPRESS;Initial Catalog=SampleProject;Integrated Security=SSPI;User ID=sa;Password=123456;";

        public static SqlParameter CreateParameter(string name, DbType dbType, object value)
        {
            return new SqlParameter {ParameterName = name, DbType = dbType, Value = value};
        }

        public static SqlCommand CreateCommand(SqlConnection conn, string sql, List<string> paramNames = null,
            List<DbType> dbTypes = null,
            List<object> parameters = null)
        {
            if (conn.State != ConnectionState.Open) conn.Open();

            var cmd = new SqlCommand(sql, conn);
            if (paramNames != null && dbTypes != null && parameters != null)
                for (var i = 0; i < paramNames.Count; i++)
                {
                    var parameter = CreateParameter(paramNames[i], dbTypes[i], parameters[i]);
                    cmd.Parameters.Add(parameter);
                }

            return cmd;
        }
    }
}