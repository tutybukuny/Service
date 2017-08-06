﻿using System.Collections.Generic;
using System.Web.Http;
using BusinessTier.Factory;
using BusinessTier.Repository;
using DataTier;

namespace Service.Controllers.Api
{
    public class ProjectApiController : ApiController
    {
        private readonly ProjectRepo _repo;

        public ProjectApiController()
        {
            _repo = (ProjectRepo)RepoFactory.GetRepo("ProjectRepo");
        }

        /// <summary>
        ///     Get all projects of user
        /// </summary>
        /// <param name="user">user's info</param>
        /// <returns></returns>
        [ActionName("GetUserProjects")]
        [HttpGet]
        public Dictionary<string, object> GetUserProjects(int user_id)
        {
            return _repo.GetUserProjects(user_id);
        }

        /// <summary>
        ///     Get first project of user
        /// </summary>
        /// <param name="user">user's info</param>
        /// <returns></returns>
        [ActionName("GetUserFirstProject")]
        [HttpPost]
        public Dictionary<string, object> GetUserFirstProject(User user)
        {
            return _repo.GetUserFirstProject(user.id);
        }

        /// <summary>
        ///     Get all projects from database
        /// </summary>
        /// <returns></returns>
        [ActionName("GetProjects")]
        [HttpPost]
        public Dictionary<string, object> GetProjects()
        {
            return _repo.GetAll();
        }
        
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [ActionName("GetCategories")]
        [HttpGet]
        public Dictionary<string, object> GetCategories()
        {
            return _repo.GetCategories();
        }
    }
}