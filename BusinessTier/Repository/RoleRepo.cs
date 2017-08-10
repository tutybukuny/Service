using System.Collections.Generic;
using DataTier.Dao;
using DataTier.Factory;

namespace BusinessTier.Repository
{
    public class RoleRepo : IRepo
    {
        private readonly RoleDao _dao;

        public RoleRepo()
        {
            _dao = (RoleDao) DaoFactory.GetDao("RoleDao");
        }

        #region Get Item

        public Dictionary<string, object> GetAll()
        {
            var dic = new Dictionary<string, object>();
            var list = _dao.GetAll();

            if (list == null) dic.Add("message", "No Role");
            dic.Add("roles", list);

            return dic;
        }

        public Dictionary<string, object> GetById(int id)
        {
            var dic = new Dictionary<string, object>();
            var role = _dao.GetById(id);

            if (role == null) dic.Add("message", "There is no role has this id!");
            dic.Add("role", role);

            return dic;
        }

        public Dictionary<string, object> GetListRoles(int[] roles)
        {
            var dic = new Dictionary<string, object>();
            var list = _dao.GetListRoles(roles);
            dic.Add("roles", list);

            return dic;
        }

        public Dictionary<string, object> GetProjectRoles(int? project_id, int limit)
        {
            var dic = new Dictionary<string, object>();
            var list = _dao.GetProjectRoles(project_id, limit);

            if (list == null) dic.Add("message", "Project has no role!");
            dic.Add("roles", list);

            return dic;
        }

        #endregion
    }
}