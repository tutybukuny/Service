using System.Collections.Generic;
using System.Web.Http;
using CoreServiceLib.Models;
using Service.Services;

namespace Service.Controllers
{
    public class UserApiController : ApiController
    {
        private readonly UserRepository _repository;

        private UserApiController()
        {
            _repository = new UserRepository();
        }

        /// <summary>
        /// Login service
        /// </summary>
        /// <param name="info">login info</param>
        /// <returns></returns>
        [ActionName("Login")]
        [HttpPost]
        public Dictionary<string, object> Login(User info)
        {
            return _repository.Login(info);
        }

        /// <summary>
        /// edit profile of user service
        /// </summary>
        /// <param name="profile">user's profile containing token</param>
        /// <returns></returns>
        [ActionName("EditProfile")]
        [HttpPost]
        public Dictionary<string, object> EditProfile(User profile)
        {
            return _repository.EditProfile(profile);
        }

        /// <summary>
        /// register service
        /// </summary>
        /// <param name="profile">user's profile containing token</param>
        /// <returns></returns>
        [ActionName("Register")]
        [HttpPost]
        public Dictionary<string, object> Register(User profile)
        {
            return _repository.Register(profile);
        }
    }
}