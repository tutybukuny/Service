using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataTier.Dao
{
    public class CountryDao : IDao<Country>
    {
        #region Insert Update Delete

        public bool Insert(Country obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Countries.Add(obj);
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Update(Country obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Countries.Attach(obj);
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

        public bool Delete(Country obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Entry(obj).State = EntityState.Deleted;

                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Get Item

        public List<Country> GetAll()
        {
            List<Country> list = null;
            using (var entities = new TheProjectEntities())
            {
                var rows = from r in entities.Countries select r;
                list = new List<Country>();

                foreach (var row in rows)
                {
                    var c = new Country
                    {
                        id = row.id,
                        name = row.name
                    };

                    list.Add(c);
                }
            }

            return list;
        }

        public Country GetById(int id)
        {
            Country country = null;
            using (var entities = new TheProjectEntities())
            {
                var row = (from r in entities.Countries where r.id == id select r).FirstOrDefault();

                if (row != null)
                    country = new Country
                    {
                        id = row.id,
                        name = row.name
                    };
            }

            return country;
        }

        #endregion
    }
}