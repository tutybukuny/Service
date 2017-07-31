using System.Collections.Generic;
using System.Web.Http;
using BusinessTier.Repository;
using DataTier;
using DataTier.Dao;
using Ninject;

namespace Service.Controllers
{
    public class UserApiController : ApiController
    {
        private UserRepo _repo;

        private UserApiController()
        {
            var kernel = new StandardKernel();
            kernel.Bind(typeof(IDao<>)).To(typeof(UserDao));
            kernel.Bind<IRepo>().To<UserRepo>();
            _repo = (UserRepo) kernel.Get<IRepo>();
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
            return _repo.Login(info);
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
            //return _repo.EditProfile(profile);
            return null;
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
            //return _repo.Register(profile);
            return null;
        }
    }
}