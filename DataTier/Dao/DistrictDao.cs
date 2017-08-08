using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataTier.Dao
{
    public class DistrictDao : IDao<District>
    {
        #region Insert Update Delete

        public bool Insert(District obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Districts.Add(obj);
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Update(District obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Districts.Attach(obj);
                    var entry = entities.Entry(obj);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.id).IsModified = false;

                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Delete(District obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Get Item

        public List<District> GetAll()
        {
            List<District> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from d in entities.Districts select d;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<District>();

                    var district = new District
                    {
                        id = row.id,
                        name = row.name,
                        state_id = row.state_id,
                        postal_code = row.postal_code
                    };
                    list.Add(district);
                }
            }

            return list;
        }

        public District GetById(int? id)
        {
            District district = null;

            using (var entities = new TheProjectEntities())
            {
                var row = (from d in entities.Districts where d.id == id select d).FirstOrDefault();

                if (row != null)
                    district = new District
                    {
                        id = row.id,
                        name = row.name,
                        state_id = row.state_id,
                        postal_code = row.postal_code
                    };
            }

            return district;
        }

        public List<District> GetByState(int? state_id)
        {
            List<District> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from d in entities.Districts where d.state_id == state_id select d;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<District>();

                    var district = new District
                    {
                        id = row.id,
                        name = row.name,
                        state_id = row.state_id,
                        postal_code = row.postal_code
                    };
                    list.Add(district);
                }
            }

            return list;
        }

        #endregion
    }
}