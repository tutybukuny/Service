using System.Collections.Generic;
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
            _repo = (ProjectRepo) RepoFactory.GetRepo("ProjectRepo");
        }

        /// <summary>
        ///     Get all projects of user
        /// </summary>
        /// <param name="user">user's info</param>
        /// <returns></returns>
        [ActionName("GetUserProjects")]
        [HttpGet]
        public Dictionary<string, object> GetUserProjects(int user_id, int limit = 10)
        {
            return _repo.GetUserProjects(user_id, limit);
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
        ///     Get all categories
        /// </summary>
        /// <returns></returns>
        [ActionName("GetCategories")]
        [HttpGet]
        public Dictionary<string, object> GetCategories()
        {
            return _repo.GetCategories();
        }

        /// <summary>
        ///     Get all projects that match filter
        /// </summary>
        /// <param name="category_id">category's id</param>
        /// <param name="sort_id">sort order</param>
        /// <param name="role_id">role's id</param>
        /// <returns></returns>
        [ActionName("GetFilteredProjects")]
        [HttpGet]
        public Dictionary<string, object> GetFilteredProjects(int category_id, int sort_id, int role_id)
        {
            return _repo.GetFilteredProject(category_id, sort_id, role_id);
        }

        /// <summary>
        ///     Join to project
        /// </summary>
        /// <param name="joinedProject"></param>
        /// <returns></returns>
        [ActionName("JoinProject")]
        [HttpPost]
        public Dictionary<string, object> JoinProject(JoinedProject joinedProject)
        {
            return _repo.JoinProject(joinedProject);
        }

        /// <summary>
        /// </summary>
        /// <param name="project_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [ActionName("IsLikedByUser")]
        [HttpGet]
        public Dictionary<string, object> IsLikedByUser(int project_id, int user_id)
        {
            return _repo.IsLikedByUser(project_id, user_id);
        }
    }
}