using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataTier.Dao
{
    public class ProjectDao : IDao<Project>
    {
        #region Insert Update Delete

        public bool Insert(Project obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Projects.Add(obj);
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Update(Project obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Projects.Attach(obj);
                    var entry = entities.Entry(obj);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.created_date).IsModified = false;

                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Delete(Project obj)
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

        public List<Project> GetAll()
        {
            List<Project> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from p in entities.Projects orderby p.created_date descending select p;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<Project>();

                    list.Add(new Project
                    {
                        id = row.id,
                        category_id = row.category_id,
                        country_id = row.country_id,
                        created_date = row.created_date,
                        description = row.description,
                        district_id = row.district_id,
                        image = row.image,
                        state_id = row.state_id,
                        title = row.title,
                        user_id = row.user_id,
                        completed = row.completed,
                        joined_people = row.joined_people,
                        people = row.people
                    });
                }
            }

            return list;
        }

        public Project GetById(int? id)
        {
            Project project = null;

            using (var entities = new TheProjectEntities())
            {
                var row = (from p in entities.Projects where p.id == id select p).FirstOrDefault();

                if (row != null)
                    project = new Project
                    {
                        id = row.id,
                        category_id = row.category_id,
                        country_id = row.country_id,
                        created_date = row.created_date,
                        description = row.description,
                        district_id = row.district_id,
                        image = row.image,
                        state_id = row.state_id,
                        title = row.title,
                        user_id = row.user_id,
                        completed = row.completed,
                        joined_people = row.joined_people,
                        people = row.people
                    };
            }

            return project;
        }

        public List<Project> GetUserProjects(int user_id)
        {
            List<Project> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from p in entities.Projects where p.user_id == user_id select p;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<Project>();

                    list.Add(new Project
                    {
                        id = row.id,
                        category_id = row.category_id,
                        country_id = row.country_id,
                        created_date = row.created_date,
                        description = row.description,
                        district_id = row.district_id,
                        image = row.image,
                        state_id = row.state_id,
                        title = row.title,
                        user_id = row.user_id,
                        completed = row.completed,
                        joined_people = row.joined_people,
                        people = row.people
                    });
                }
            }

            return list;
        }

        public Project GetUserFirstProject(int user_id)
        {
            Project project = null;

            using (var entities = new TheProjectEntities())
            {
                var row = (from p in entities.Projects where p.user_id == user_id select p).FirstOrDefault();

                if (row != null)
                    project = new Project
                    {
                        id = row.id,
                        category_id = row.category_id,
                        country_id = row.country_id,
                        created_date = row.created_date,
                        description = row.description,
                        district_id = row.district_id,
                        image = row.image,
                        state_id = row.state_id,
                        title = row.title,
                        user_id = row.user_id,
                        completed = row.completed,
                        joined_people = row.joined_people,
                        people = row.people
                    };
            }

            return project;
        }

        public List<Project> GetFilteredProjects(int category_id, int sort_id, int role_id)
        {
            List<Project> list = null;

            using (var entities = new TheProjectEntities())
            {
                var projects = from p in entities.Projects
                    join pr in entities.ProjectRoles on p.id equals pr.project_id
                    join r in entities.Roles on pr.role_id equals r.id
                    where p.category_id == (category_id == 0 ? p.category_id : category_id)
                          && r.id == (role_id == 0 ? r.id : role_id)
                    group p by p.id
                    into prj
                    select prj;

                foreach (var rows in projects)
                foreach (var row in rows)
                {
                    if (list == null) list = new List<Project>();

                    list.Add(new Project
                    {
                        id = row.id,
                        category_id = row.category_id,
                        country_id = row.country_id,
                        created_date = row.created_date,
                        description = row.description,
                        district_id = row.district_id,
                        image = row.image,
                        state_id = row.state_id,
                        title = row.title,
                        user_id = row.user_id,
                        completed = row.completed,
                        joined_people = row.joined_people,
                        people = row.people
                    });

                    break;
                }
            }

            return list;
        }

        #endregion
    }
}