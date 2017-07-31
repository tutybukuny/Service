using System.Collections.Generic;
using DataTier;
using DataTier.Dao;

namespace BusinessTier.Repository
{
    public class UserRepo : IRepo
    {
        private readonly UserDao _dao;

        public UserRepo(IDao<User> dao)
        {
            _dao = (UserDao) dao;
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
                var user = _dao.Login(info.email, info.password);

                if (user == null)
                    dic.Add("message", "Wrong email or password!");
                else
                    dic.Add("user", user);
            }
            else
            {
                dic.Add("messages", messages);
            }


            return dic;
        }
    }
}