using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTier.Dao
{
    public class RoleDao : IDao<Role>
    {
        #region Insert Update Delete

        public bool Insert(Role obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(Role obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Role obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Get Item

        public List<Role> GetAll()
        {
            List<Role> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from r in entities.Roles select r;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<Role>();

                    list.Add(new Role
                    {
                        id = row.id,
                        role = row.role
                    });
                }
            }

            return list;
        }

        public Role GetById(int id)
        {
            Role role = null;

            using (var entities = new TheProjectEntities())
            {
                var row = (from r in entities.Roles where r.id == id select r).FirstOrDefault();

                if (row != null)
                    role = new Role
                    {
                        id = row.id,
                        role = row.role
                    };
            }

            return role;
        }

        public List<Role> GetListRoles(int[] roles)
        {
            List<Role> list = new List<Role>();

            foreach (var i in roles)
            {
                Role role = GetById(i);
                list.Add(role);
            }

            return list;
        }

        #endregion
    }
}