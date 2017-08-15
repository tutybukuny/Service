using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace DataTier.Dao
{
    public class FollowingProjectDao : IDao<FollowingProject>
    {
        #region Insert Update Delete

        public bool Insert(FollowingProject obj)
        {
            try
            {
                var conn = new SqlConnection(DaoLib.ConnectionString);
                conn.Open();
                var paramNames = new List<string> {"@user_id", "@project_id", "@created_date"};
                var dbTypes = new List<DbType>
                {
                    DbType.Int32,
                    DbType.Int32,
                    DbType.DateTime
                };
                var values = new List<object> {obj.user_id, obj.project_id, DateTime.Now};
                var sql = "INSERT INTO dbo.[FollowingProject](user_id, project_id, created_date) " +
                          "VALUES(@user_id, @project_id, @created_date)";
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

        public bool Update(FollowingProject obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(FollowingProject obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Entry(obj).State = EntityState.Deleted;
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Delete(int user_id, int project_id)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    var row = entities.FollowingProjects.FirstOrDefault(
                        f => f.user_id == user_id && f.project_id == project_id);

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

        public List<FollowingProject> GetAll()
        {
            throw new NotImplementedException();
        }

        public FollowingProject GetById(int? id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}