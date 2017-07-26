using System.Collections.Generic;
using System.Web.Http;
using CoreServiceLib.Models;
using Service.Models;
using Service.Services;

namespace Service.Controllers
{
    public class UserApiController : ApiController
    {
        private readonly UserService service;

        private UserApiController()
        {
            service = new UserService();
        }

        [ActionName("Login")]
        [HttpPost]
        public Dictionary<string, object> Login(User info)
        {
            return service.Login(info);
        }

        [ActionName("EditProfile")]
        [HttpPost]
        public Dictionary<string, object> EditProfile(User user)
        {
            return service.EditProfile(user);
        }
    }
}