using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Castle.Components.DictionaryAdapter;

namespace DataTier.Dao
{
    public class CategoryDao : IDao<Category>
    {
        #region Insert Update Delete

        public bool Insert(Category obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Categories.Add(obj);
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Update(Category obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Categories.Attach(obj);
                    var entry = entities.Entry(obj);
                    entry.State = EntityState.Modified;
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Delete(Category obj)
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

        public List<Category> GetAll()
        {
            List<Category> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from c in entities.Categories select c;

                foreach (var row in rows)
                {
                    if (list == null) list = new EditableList<Category>();

                    list.Add(new Category
                    {
                        id = row.id,
                        category = row.category,
                        description = row.description
                    });
                }
            }

            return list;
        }

        public Category GetById(int? id)
        {
            Category category = null;

            using (var entities = new TheProjectEntities())
            {
                var row = (from c in entities.Categories where c.id == id select c).FirstOrDefault();

                if (row != null)
                {
                    category = new Category
                    {
                        id = row.id,
                        category = row.category,
                        description = row.description
                    };
                }
            }

            return category;
        }

        #endregion
    }
}