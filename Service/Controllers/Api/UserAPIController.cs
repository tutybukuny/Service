﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using BusinessTier.Repository;
using DataTier;
using Newtonsoft.Json.Linq;
using Ninject;

namespace Service.Controllers.Api
{
    public class UserApiController : ApiController
    {
        private readonly UserRepo _repo;

        private UserApiController()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IRepo>().To<UserRepo>();
            _repo = (UserRepo) kernel.Get<IRepo>();
        }

        /// <summary>
        ///     Login service
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
        ///     edit profile of user service
        /// </summary>
        /// <param name="data">data</param>
        /// <returns></returns>
        [ActionName("EditProfile")]
        [HttpPost]
        public Dictionary<string, object> EditProfile(JObject data)
        {
            var user = new User
            {
                email = (string) data["email"],
                password = (string) data["password"],
                about_me = (string) data["about_me"],
                avatar = (string) data["avatar"],
                birthday = (DateTime?) data["birthday"],
                country_id = (int?) data["country_id"],
                district_id = (int?) data["district_id"],
                firstname = (string) data["firstname"],
                lastname = (string) data["lastname"],
                postal_code = (string) data["postal_code"],
                state_id = (int?) data["state_id"],
                role1 = (int?) data["role1"],
                role2 = (int?) data["role2"]
            };

            return _repo.EditProfile(user, (string) data["token"]);
        }

        /// <summary>
        ///     register service
        /// </summary>
        /// <param name="profile">user's profile containing token</param>
        /// <returns></returns>
        [ActionName("Register")]
        [HttpPost]
        public Dictionary<string, object> Register(User profile)
        {
            return _repo.Register(profile);
        }
    }
}