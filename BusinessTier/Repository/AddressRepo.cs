using System.Collections.Generic;
using DataTier.Dao;
using DataTier.Factory;

namespace BusinessTier.Repository
{
    public class AddressRepo : IRepo
    {
        private readonly CountryDao _countryDao;
        private readonly DistrictDao _districtDao;
        private readonly StateDao _stateDao;

        public AddressRepo()
        {
            _countryDao = (CountryDao) DaoFactory.GetDao("CountryDao");
            _stateDao = (StateDao) DaoFactory.GetDao("StateDao");
            _districtDao = (DistrictDao) DaoFactory.GetDao("DistrictDao");
        }

        public Dictionary<string, object> GetAddress(int? country_id, int? state_id, int? district_id)
        {
            var dic = new Dictionary<string, object>();
            var messages = new List<string>();

            var country = _countryDao.GetById(country_id);
            var state = _stateDao.GetById(state_id);
            var district = _districtDao.GetById(district_id);

            if (country == null)
                messages.Add("Country is null");

            if (state == null)
                messages.Add("State is null");

            if (district == null)
                messages.Add("District is null");

            dic.Add("messages", messages);
            dic.Add("country", country);
            dic.Add("state", state);
            dic.Add("district", district);

            return dic;
        }
    }
}