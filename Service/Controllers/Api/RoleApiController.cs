using System.Collections.Generic;
using System.Web.Http;
using BusinessTier.Factory;
using BusinessTier.Repository;

namespace Service.Controllers.Api
{
    public class RoleApiController : ApiController
    {
        private readonly RoleRepo _repo;

        public RoleApiController()
        {
            _repo = (RoleRepo) RepoFactory.GetRepo("RoleRepo");
        }

        [ActionName("GetRoles")]
        [HttpGet]
        public Dictionary<string, object> GetRoles()
        {
            return _repo.GetAll();
        }

        [ActionName("GetById")]
        [HttpGet]
        public Dictionary<string, object> GetById(int id)
        {
            return _repo.GetById(id);
        }

        [ActionName("GetProjectRoles")]
        [HttpGet]
        public Dictionary<string, object> GetProjectRoles(int? project_id, int limit = 3)
        {
            return _repo.GetProjectRoles(project_id, limit);
        }
    }
}