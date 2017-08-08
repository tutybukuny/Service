using System.Collections.Generic;
using DataTier;
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

        #region Get Address

        /// <summary>
        ///     Get Address
        /// </summary>
        /// <param name="country_id"></param>
        /// <param name="state_id"></param>
        /// <param name="district_id"></param>
        /// <returns></returns>
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

        #endregion

        #region Get Countries

        public Dictionary<string, object> GetCountries()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<Country> list = _countryDao.GetAll();
            
            if(list==null) dic.Add("message", "There is no country in the world!");
            dic.Add("countries", list);

            return dic;
        }

        #endregion

        #region Get States

        public Dictionary<string, object> GetStates(int? country_id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<State> list = _stateDao.GetByCountry(country_id);

            if (list == null) dic.Add("message", "There is no state!");
            dic.Add("states", list);

            return dic;
        }

        #endregion

        #region Get Districts

        public Dictionary<string, object> GetDistricts(int? state_id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<District> list = _districtDao.GetByState(state_id);

            if (list == null) dic.Add("message", "There is no district!");
            dic.Add("districts", list);

            return dic;
        }

        #endregion
    }
}