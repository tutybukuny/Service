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

        [ActionName("Login")]
        [HttpPost]
        public Dictionary<string, object> Login(User info)
        {
            return _repository.Login(info);
        }

        [ActionName("EditProfile")]
        [HttpPost]
        public Dictionary<string, object> EditProfile(User user)
        {
            return _repository.EditProfile(user);
        }
    }
}