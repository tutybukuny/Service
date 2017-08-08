using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataTier.Dao
{
    public class StateDao : IDao<State>
    {
        #region Insert Update Delete

        public bool Insert(State obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.States.Add(obj);
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Update(State obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.States.Attach(obj);
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

        public bool Delete(State obj)
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

        public List<State> GetAll()
        {
            List<State> list = null;
            using (var entities = new TheProjectEntities())
            {
                var rows = from r in entities.States select r;
                list = new List<State>();

                foreach (var row in rows)
                {
                    var c = new State
                    {
                        id = row.id,
                        name = row.name,
                        country_id = row.country_id
                    };

                    list.Add(c);
                }
            }

            return list;
        }

        public State GetById(int? id)
        {
            State state = null;
            using (var entities = new TheProjectEntities())
            {
                var row = (from r in entities.States where r.id == id select r).FirstOrDefault();

                if (row != null)
                    state = new State
                    {
                        id = row.id,
                        name = row.name,
                        country_id = row.country_id
                    };
            }

            return state;
        }

        public List<State> GetByCountry(int? country_id)
        {
            List<State> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from s in entities.States where s.country_id == country_id select s;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<State>();

                    list.Add(new State
                    {
                        id = row.id,
                        country_id = row.country_id,
                        name = row.name
                    });
                }
            }

            return list;
        }

        #endregion
    }
}