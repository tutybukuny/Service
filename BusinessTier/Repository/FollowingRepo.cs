using System.Collections.Generic;
using DataTier;
using DataTier.Dao;
using DataTier.Factory;

namespace BusinessTier.Repository
{
    public class FollowingRepo : IRepo
    {
        private readonly FollowingDao _followingDao;
        private readonly TokenDao _tokenDao;

        public FollowingRepo()
        {
            _followingDao = (FollowingDao) DaoFactory.GetDao("FollowingDao");
            _tokenDao = (TokenDao) DaoFactory.GetDao("TokenDao");
        }

        #region Follow

        public Dictionary<string, object> Follow(string token, int user_id, bool follow)
        {
            var dic = new Dictionary<string, object>();
            var follower_id = _tokenDao.GetUserId(token);
            var success = follow
                ? _followingDao.Insert(new Following {follower_id = follower_id, user_id = user_id})
                : _followingDao.Delete(follower_id, user_id);

            dic.Add("success", success);

            return dic;
        }

        #endregion

        #region Get Followers

        public Dictionary<string, object> GetFollwers(int user_id)
        {
            var list = _followingDao.GetFollowers(user_id);

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
            var list = _followingDao.GetFollowings(follower_id);

            return new Dictionary<string, object>
            {
                {"followings", list},
                {"message", list == null ? "User doesn't has any Followings!" : "Successful!"}
            };
        }

        #endregion

        #region Is followed

        public Dictionary<string, object> IsFollowed(int follower_id, int user_id)
        {
            return new Dictionary<string, object>
            {
                {"followed", _followingDao.IsFollowed(follower_id, user_id)}
            };
        }

        #endregion
    }
}