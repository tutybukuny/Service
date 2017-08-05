using System.Collections.Generic;
using DataTier;
using DataTier.Dao;
using DataTier.Factory;

namespace BusinessTier.Repository
{
    public class FollowingRepo : IRepo
    {
        private readonly FollowingDao _dao;

        public FollowingRepo()
        {
            _dao = (FollowingDao) DaoFactory.GetDao("FollowingDao");
        }

        #region Insert

        public Dictionary<string, object> Insert(Following following)
        {
            var dic = new Dictionary<string, object>
            {
                {"message", !_dao.Insert(following) ? "Something went wrong!" : "Successful!"}
            };


            return dic;
        }

        #endregion

        #region Delete

        public Dictionary<string, object> Delete(Following following)
        {
            var dic = new Dictionary<string, object>
            {
                {"message", _dao.Delete(following) ? "Successful!" : "Something went wrong!"}
            };


            return dic;
        }

        #endregion

        #region Get Followers

        public Dictionary<string, object> GetFollwers(int user_id)
        {
            var list = _dao.GetFollowers(user_id);

            return new Dictionary<string, object>
            {
                {"followers", list},
                {"message", list == null ? "User doesn't has any follower!" : "Successful!"}
            };
        }

        #endregion

        #region Get Followings

        public Dictionary<string, object> GetFollowings(int follower_id)
        {
            var list = _dao.GetFollowings(follower_id);

            return new Dictionary<string, object>
            {
                {"followings", list},
                {"message", list == null ? "User doesn't has any Followings!" : "Successful!"}
            };
        }

        #endregion
    }
}