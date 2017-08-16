using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using DataTier.Factory;

namespace DataTier.Dao
{
    public class FollowingDao : IDao<Following>
    {
        #region Insert Update Delete

        public bool Insert(Following obj)
        {
            try
            {
                var conn = new SqlConnection(DaoLib.ConnectionString);
                conn.Open();
                var paramNames = new List<string> {"@follower_id", "@user_id", "@created_date"};
                var dbTypes = new List<DbType>
                {
                    DbType.Int32,
                    DbType.Int32,
                    DbType.DateTime
                };
                var values = new List<object> {obj.follower_id, obj.user_id, DateTime.Now};
                var sql = "INSERT INTO dbo.[Following](follower_id, user_id, created_date) " +
                          "VALUES(@follower_id, @user_id, @created_date)";
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

        public bool Update(Following obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Followings.Attach(obj);
                    var entry = entities.Entry(obj);
                    entry.State = EntityState.Modified;
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Delete(Following obj)
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

        public bool Delete(int follower_id, int user_id)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    var row = entities.Followings.FirstOrDefault(
                        f => f.user_id == user_id && f.follower_id == follower_id);

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

        public List<Following> GetAll()
        {
            throw new NotImplementedException();
        }

        public Following GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetFollowers(int user_id)
        {
            List<User> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from f in entities.Followings where f.user_id == user_id select f;
                var userDao = (UserDao) DaoFactory.GetDao("UserDao");

                foreach (var row in rows)
                {
                    if (list == null) list = new List<User>();

                    list.Add(userDao.GetById(row.follower_id));
                }
            }

            return list;
        }

        public List<User> GetFollowings(int follower_id)
        {
            List<User> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from f in entities.Followings where f.follower_id == follower_id select f;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<User>();
                    var userDao = (UserDao) DaoFactory.GetDao("UserDao");

                    list.Add(userDao.GetById(row.user_id));
                }
            }

            return list;
        }

        public bool IsFollowed(int follower_id, int user_id)
        {
            bool result;

            using (var entities = new TheProjectEntities())
            {
                var row = entities.Followings.FirstOrDefault(f => f.user_id == user_id && f.follower_id == follower_id);

                result = row != null;
            }

            return result;
        }

        #endregion
    }
}