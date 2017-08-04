using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataTier.Dao
{
    public class FollowingDao : IDao<Following>
    {
        #region Insert Update Delete

        public bool Insert(Following obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Followings.Add(obj);
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
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

        public List<Following> GetFollowers(int user_id)
        {
            List<Following> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from f in entities.Followings where f.user_id == user_id select f;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<Following>();

                    list.Add(new Following
                    {
                        created_date = row.created_date,
                        follower_id = row.follower_id,
                        id = row.id,
                        user_id = row.user_id
                    });
                }
            }

            return list;
        }

        public List<Following> GetFollowings(int follower_id)
        {
            List<Following> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from f in entities.Followings where f.follower_id == follower_id select f;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<Following>();

                    list.Add(new Following
                    {
                        created_date = row.created_date,
                        follower_id = row.follower_id,
                        id = row.id,
                        user_id = row.user_id
                    });
                }
            }

            return list;
        }

        #endregion
    }
}