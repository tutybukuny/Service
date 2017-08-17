using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace DataTier.Dao
{
    public class JoinedProjectDao : IDao<JoinedProject>
    {
        #region Insert Update Delete

        public bool Insert(JoinedProject obj)
        {
            try
            {
                var conn = new SqlConnection(DaoLib.ConnectionString);
                conn.Open();
                var paramNames = new List<string> {"@user_id", "@project_id", "@role_id", "@created_date"};
                var dbTypes = new List<DbType>
                {
                    DbType.Int32,
                    DbType.Int32,
                    DbType.Int32,
                    DbType.DateTime
                };
                var values = new List<object> {obj.user_id, obj.project_id, obj.role_id, DateTime.Now};
                var sql = "INSERT INTO dbo.[JoinedProject](user_id, project_id, role_id, created_date) " +
                          "VALUES(@user_id, @project_id, @role_id, @created_date)";
                var cmd = DaoLib.CreateCommand(conn, sql, paramNames, dbTypes, values);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Update(JoinedProject obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(JoinedProject obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    var row = entities.JoinedProjects.FirstOrDefault(
                        j => j.user_id == obj.user_id && j.project_id == obj.project_id);

                    if (row != null)
                    {
                        entities.Entry(row).State = EntityState.Deleted;
                        entities.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Get Item

        public List<JoinedProject> GetAll()
        {
            throw new NotImplementedException();
        }

        public JoinedProject GetById(int? id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}