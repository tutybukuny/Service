using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTier.Dao
{
    public class LikeDao : IDao<Like>
    {
        #region Insert Update Delete

        public bool Insert(Like obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(Like obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Like obj)
        {
            throw new NotImplementedException();
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