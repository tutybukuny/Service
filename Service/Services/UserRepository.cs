using System.Collections.Generic;
using System.Data.SqlClient;
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

        #region Login action

        /// <summary>
        ///     action when user logins
        /// </summary>
        /// <param name="info">login info</param>
        /// <returns></returns>
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

        #endregion

        #region Edit profile action

        /// <summary>
        ///     action when user edits profile
        /// </summary>
        /// <param name="profile">user's profile</param>
        /// <returns></returns>
        public Dictionary<string, object> EditProfile(User profile)
        {
            Dictionary<string, object> dic;
            profile.id = _tokenDao.CheckToken(profile.token);

            if (profile.id == -1)
            {
                dic = new Dictionary<string, object>
                {
                    {"error", true},
                    {"message", "Wrong token! You must login first!"}
                };
            }
            else
            {
                _userDao.Update(profile);

                dic = new Dictionary<string, object>
                {
                    {"success", true},
                    {"message", "Updated profile!"}
                };
            }

            return dic;
        }

        #endregion

        #region Register action

        /// <summary>
        /// Register action
        /// </summary>
        /// <param name="profile">profile of user</param>
        /// <returns></returns>
        public Dictionary<string, object> Register(User profile)
        {
            var dic = new Dictionary<string, object>();

            try
            {
                _userDao.Insert(profile);
                dic.Add("success", true);
                dic.Add("message", "New User has been created!");
            }
            catch (SqlException e)
            {
                dic.Add("error", true);
                dic.Add("message", "Something went wrong when creating new User!");
            }

            return dic;
        }

        #endregion
    }
}