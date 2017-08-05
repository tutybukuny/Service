using System.Collections.Generic;
using System.Web.Http;
using BusinessTier.Factory;
using BusinessTier.Repository;

namespace Service.Controllers.Api
{
    public class AddressApiController : ApiController
    {
        private readonly AddressRepo _repo;

        public AddressApiController()
        {
            _repo = (AddressRepo) RepoFactory.GetRepo("AddressRepo");
        }

        [ActionName("GetAddress")]
        [HttpGet]
        public Dictionary<string, object> GetAddress(int? country_id, int? state_id, int? district_id)
        {
            return _repo.GetAddress(country_id, state_id, district_id);
        }
    }
}