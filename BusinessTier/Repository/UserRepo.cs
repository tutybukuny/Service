using System;
using System.Collections.Generic;
using BusinessTier.Core;
using DataTier;
using DataTier.Dao;
using DataTier.Factory;

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

        #region Login

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

        #endregion

        #region Register

        public Dictionary<string, object> Register(User info)
        {
            var dic = new Dictionary<string, object>();
            var messages = new List<string>();
            var success = true;

            #region Validate empty

            if (string.IsNullOrEmpty(info.email))
            {
                success = false;
                messages.Add("Email can't be empty!");
            }

            if (string.IsNullOrEmpty(info.password))
            {
                success = false;
                messages.Add("Password can't by empty!");
            }

            if (string.IsNullOrEmpty(info.firstname))
            {
                success = false;
                messages.Add("Firstname can't by empty!");
            }

            if (string.IsNullOrEmpty(info.lastname))
            {
                success = false;
                messages.Add("Lastname can't by empty!");
            }

            #endregion

            if (success)
                if (!_userDao.IsExistedEmail(info.email))
                {
                    info.created_date = DateTime.Now;
                    if (_userDao.Insert(info))
                    {
                        dic.Add("message", "Created new account!");
                    }
                    else
                    {
                        success = false;
                        dic.Add("message", "Something went wrong!");
                    }
                }
                else
                {
                    dic.Add("message", "Existed email!");
                    success = false;
                }
            else
                dic.Add("messages", messages);

            dic.Add("success", success);

            return dic;
        }

        #endregion

        #region Check Password

        public Dictionary<string, object> CheckPassword(int? user_id, string password)
        {
            var dic = new Dictionary<string, object>();
            var success = _userDao.CheckPassword(user_id, password);
            dic.Add("message", success ? "True password!" : "Wrong password!");
            dic.Add("success", success);

            return dic;
        }

        #endregion

        #region User Info

        public Dictionary<string, object> GetUserInfo(int? user_id)
        {
            var dic = new Dictionary<string, object>();
            var user = _userDao.GetById(user_id);

            if (user == null) dic.Add("message", "No user likes this!");
            dic.Add("user", user);

            return dic;
        }

        #endregion

        #region Get By Token

        public Dictionary<string, object> GetByToken(string token)
        {
            var dic = new Dictionary<string, object>();
            var user = _userDao.GetByToken(token);

            if (user == null) dic.Add("message", "No user likes this!");
            dic.Add("user", user);

            return dic;
        }

        #endregion

        #region EditProfile

        public Dictionary<string, object> EditProfile(User profile, string token)
        {
            var dic = new Dictionary<string, object>();
            var messages = new List<string>();
            var success = true;

            var user_id = _tokenDao.GetUserId(token);

            if (user_id == -1)
            {
                success = false;
                messages.Add("Invalid token!");
            }

            if (success)
            {
                profile.id = user_id;
                if (_userDao.Update(profile))
                {
                    dic.Add("message", "Update Success!");
                }
                else
                {
                    dic.Add("message", "Something went wrong!");
                    success = false;
                }
            }
            else
            {
                dic.Add("messages", messages);
            }

            dic.Add("success", success);

            return dic;
        }

        public Dictionary<string, object> UpdatePassword(int? user_id, string old_password, string new_password)
        {
            var dic = new Dictionary<string, object>();
            var success = _userDao.UpdatePassword(user_id, old_password, new_password);
            dic.Add("message", success ? "Password updated!" : "Something went wrong!");
            dic.Add("success", success);

            return dic;
        }

        #endregion

        #region User Profile

        public Dictionary<string, object> UserProfile(int user_id)
        {
            var dic = new Dictionary<string, object>();
            var user = _userDao.GetById(user_id);
            if (user == null) dic.Add("message", "No user likes this");
            dic.Add("user", user);
            return dic;
        }

        public void ChangeAvatar(int? user_id, string avatar)
        {
            _userDao.UpdateAvatar(user_id, avatar);
        }

        #endregion
    }
}