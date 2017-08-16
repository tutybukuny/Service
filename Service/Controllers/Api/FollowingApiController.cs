using System.Collections.Generic;
using System.Web.Http;
using BusinessTier.Factory;
using BusinessTier.Repository;
using DataTier;

namespace Service.Controllers.Api
{
    public class FollowingApiController : ApiController
    {
        private readonly FollowingRepo _repo;

        public FollowingApiController()
        {
            _repo = (FollowingRepo) RepoFactory.GetRepo("FollowingRepo");
        }

        [ActionName("Follow")]
        [HttpGet]
        public Dictionary<string, object> Follow(string token, int user_id, bool follow)
        {
            return _repo.Follow(token, user_id, follow);
        }

        [ActionName("GetFollowers")]
        [HttpGet]
        public Dictionary<string, object> GetFollowers(int user_id)
        {
            return _repo.GetFollwers(user_id);
        }

        [ActionName("GetFollowings")]
        [HttpGet]
        public Dictionary<string, object> GetFollowings(int follower_id)
        {
            return _repo.GetFollowings(follower_id);
        }

        [ActionName("IsFollowed")]
        [HttpGet]
        public Dictionary<string, object> IsFollowed(int follower_id, int user_id)
        {
            return _repo.IsFollowed(follower_id, user_id);
        }
    }
}