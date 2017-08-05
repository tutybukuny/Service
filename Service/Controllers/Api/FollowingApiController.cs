using System;
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
        [HttpPost]
        public Dictionary<string, object> Follow(Following following)
        {
            following.created_date = DateTime.Now;
            return _repo.Insert(following);
        }

        [ActionName("Unfollow")]
        [HttpPost]
        public Dictionary<string, object> Unfollow(Following following)
        {
            return _repo.Delete(following);
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
    }
}