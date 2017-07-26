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

        #region initial and destroy connection
        /// <summary>
        /// open connection to database
        /// </summary>
        public void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
        }

        /// <summary>
        /// close connection
        /// </summary>
        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }

        /// <summary>
        /// dispose connection
        /// </summary>
        public void DisposeConnection()
        {
            if (conn != null) conn.Dispose();
        }
        #endregion

        #region insert update delete
        public abstract bool Insert(T obj);
        public abstract bool Delete(T obj);
        public abstract bool Update(T obj);
        #endregion

        #region get items
        /// <summary>
        /// get all item form table
        /// </summary>
        /// <returns></returns>
        public abstract List<T> GetAll();
        /// <summary>
        /// get item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract T GetById(int id);
        #endregion

        #region generate sql command
        /// <summary>
        /// Create parameter for SqlCommand
        /// </summary>
        /// <param name="name">name of parameter</param>
        /// <param name="dbType">DbType of parameter</param>
        /// <param name="value">value of parameter</param>
        /// <returns></returns>
        protected SqlParameter CreateParameter(string name, DbType dbType, object value)
        {
            return new SqlParameter { ParameterName = name, DbType = dbType, Value = value };
        }

        /// <summary>
        /// Create Command
        /// </summary>
        /// <param name="sql">sql command</param>
        /// <param name="paramNames">names for each parameter</param>
        /// <param name="dbTypes">Dbtypes for each parameter</param>
        /// <param name="values">values for each parameter</param>
        /// <returns></returns>
        protected SqlCommand CreateCommand(string sql, List<string> paramNames = null, List<DbType> dbTypes = null,
            List<object> values = null)
        {
            if (conn.State != ConnectionState.Open) conn.Open();

            var cmd = new SqlCommand(sql, conn);
            if (paramNames != null && dbTypes != null && values != null)
                for (var i = 0; i < paramNames.Count; i++)
                {
                    var parameter = CreateParameter(paramNames[i], dbTypes[i], values[i]);
                    cmd.Parameters.Add(parameter);
                }

            return cmd;
        }

        /// <summary>
        /// automatic generating values
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected List<object> CreateValues(object obj)
        {
            var values = new List<object>();
            var type = obj.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
                values.Add(property.GetValue(obj, null));

            return values;
        }
        #endregion
    }
}