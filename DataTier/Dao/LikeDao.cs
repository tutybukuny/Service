using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace DataTier.Dao
{
    public class LikeDao : IDao<Like>
    {
        #region Insert Update Delete

        public bool Insert(Like obj)
        {
            try
            {
                var conn = new SqlConnection(DaoLib.ConnectionString);
                conn.Open();
                var paramNames = new List<string>
                {
                    "@user_id",
                    "@project_id",
                    "@created_date"
                };
                var dbTypes = new List<DbType>
                {
                    DbType.Int32,
                    DbType.Int32,
                    DbType.DateTime,
                };
                var values = new List<object>
                {
                    obj.user_id,
                    obj.project_id,
                    DateTime.Now
                };
                var sql =
                    "INSERT INTO dbo.[Like](user_id, project_id, created_date) " +
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

        public bool Update(Like obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Like obj)
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
            return true;
        }

        #endregion

        #region Get Item

        public List<Like> GetAll()
        {
            throw new NotImplementedException();
        }

        public Like GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool IsLikedByUser(int project_id, int user_id)
        {
            bool result;

            using (var entities = new TheProjectEntities())
            {
                var row = entities.Likes.FirstOrDefault(l => l.project_id == project_id && l.user_id == user_id);
                result = row != null;
            }

            return result;
        }

        #endregion
    }
}