using System;
using System.Collections.Generic;
using BusinessTier.Core;
using BusinessTier.Factory;
using DataTier;
using DataTier.Dao;

namespace BusinessTier.Repository
{
    public class UserRepo : IRepo
    {
        private readonly TokenDao _tokenDao;
        private readonly UserDao _userDao;

        public UserRepo()
        {
            _userDao = (UserDao) DaoFactory.GetDao("UserDao");
            _tokenDao = (TokenDao) DaoFactory.GetDao("TokenDao");
        }

        public Dictionary<string, object> Login(User info)
        {
            var dic = new Dictionary<string, object>();
            var messages = new List<string>();
            var success = true;

            if (string.IsNullOrEmpty(info.email))
            {
                success = false;
                messages.Add("Email cannot be empty!");
            }

            if (string.IsNullOrEmpty(info.password))
            {
                success = false;
                messages.Add("Password cannot be empty!");
            }

            if (success)
            {
                var user = _userDao.Login(info.email, info.password);

                if (user == null)
                {
                    dic.Add("message", "Wrong email or password!");
                }
                else
                {
                    dic.Add("user", user);
                    var token = TokenGen.AutoGenerate();
                    dic.Add("token", token);

                    _tokenDao.Insert(new Token {token = token, user_id = user.id, created_date = DateTime.Now});
                }
            }
            else
            {
                dic.Add("messages", messages);
            }


            return dic;
        }
    }
}