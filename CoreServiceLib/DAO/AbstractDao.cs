using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CoreServiceLib.DAO
{
    public abstract class AbstractDao<T> where T : class
    {
        protected SqlConnection conn;
        protected string connectStr;

        protected AbstractDao(string connectStr)
        {
            conn = new SqlConnection(connectStr);
            this.connectStr = connectStr;
        }

        public void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
        }

        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }

        public void DisposeConnection()
        {
            if (conn != null) conn.Dispose();
        }

        protected SqlCommand CreateCommand(string sql)
        {
            return new SqlCommand(sql, conn);
        }

        public abstract bool Insert(T obj);
        public abstract bool Delete(T obj);
        public abstract bool Update(T obj);

        public abstract List<T> GetAll();
        public abstract T GetById(int id);

        protected SqlParameter CreateParameter(string name, DbType dbType, object value)
        {
            return new SqlParameter {ParameterName = name, DbType = dbType, Value = value};
        }

        protected SqlCommand CreateCommand(string sql, List<string> paramNames = null, List<DbType> dbTypes = null,
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

        protected List<object> CreateValues(object obj)
        {
            var values = new List<object>();
            var type = obj.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
                values.Add(property.GetValue(obj, null));

            return values;
        }
    }
}