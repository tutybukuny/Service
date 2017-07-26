using System.Collections.Generic;
using System.Web.Http;
using CoreServiceLib.DAO;
using CoreServiceLib.Models;
using Service.Models;
using Service.Properties;

namespace Service.Controllers
{
    public class UserApiController : ApiController
    {
        private readonly UserDao userDao;
        private readonly TokenDao tokenDao;

        private UserApiController()
        {
            userDao = new UserDao(Settings.Default.ConnectionString);
            tokenDao = new TokenDao(Settings.Default.ConnectionString);
        }

        [ActionName("Login")]
        [HttpPost]
        public Dictionary<string, object> Login(JsonUser info)
        {
            var user = userDao.GetUserByLoginInfo(info.email, info.password);

            Dictionary<string, object> dic;

            if (user == null)
            {
                dic = new Dictionary<string, object> {{"error", true}, {"message", "Wrong email or password"}};
            }
            else
            {
                var token = tokenDao.AutoGenerate();
                tokenDao.Insert(new Token {TokenString = token, User = user});
                dic = new Dictionary<string, object> {{"user", user}, {"token", token}};
            }

            return dic;
        }

        [ActionName("EditProfile")]
        [HttpPost]
        public JsonUser EditProfile(JsonUser user)
        {
            return user;
        }
    }
}