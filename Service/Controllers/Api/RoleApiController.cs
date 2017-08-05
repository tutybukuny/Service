using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        [ActionName("GetAll")]
        [HttpPost]
        public Dictionary<string, object> GetAll()
        {
            return _repo.GetAll();
        }

        [ActionName("GetById")]
        [HttpGet]
        public Dictionary<string, object> GetById(int id)
        {
            return _repo.GetById(id);
        }
    }
}
