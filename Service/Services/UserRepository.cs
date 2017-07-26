using System.Collections.Generic;
using CoreServiceLib.DAO;
using CoreServiceLib.Models;
using Service.Properties;

namespace Service.Services
{
    public class UserRepository
    {
        private readonly TokenDao _tokenDao;
        private readonly UserDao _userDao;

        public UserRepository()
        {
            _userDao = new UserDao(Settings.Default.ConnectionString);
            _tokenDao = new TokenDao(Settings.Default.ConnectionString);
        }

        public Dictionary<string, object> Login(User info)
        {
            var user = _userDao.GetUserByLoginInfo(info.email, info.password);

            Dictionary<string, object> dic;

            if (user == null)
            {
                dic = new Dictionary<string, object> {{"error", true}, {"message", "Wrong email or password"}};
            }
            else
            {
                var token = _tokenDao.AutoGenerate();
                _tokenDao.Insert(new Token {TokenString = token, User = user});
                dic = new Dictionary<string, object> {{"user", user}, {"token", token}};
            }

            return dic;
        }

        public Dictionary<string, object> EditProfile(User user)
        {
            Dictionary<string, object> dic;
            user.id = _tokenDao.CheckToken(user.token);

            if (user.id == -1)
            {
                dic = new Dictionary<string, object>
                {
                    {"error", true},
                    {"message", "Wrong token! You must login first!"}
                };
            }
            else
            {
                _userDao.Update(user);

                dic = new Dictionary<string, object>
                {
                    {"success", true},
                    {"message", "Updated profile!"}
                };
            }

            return dic;
        }
    }
}