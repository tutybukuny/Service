﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataTier.Dao
{
    public class UserDao : IDao<User>
    {
        #region Check User

        public bool IsExistedEmail(string email)
        {
            bool result;

            using (var entities = new TheProjectEntities())
            {
                var row = (from u in entities.Users where u.email == email select u).FirstOrDefault();
                result = row != null;
            }

            return result;
        }

        #endregion

        #region Insert Update Delete

        public bool Insert(User obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Users.Add(obj);
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Update(User obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Users.Attach(obj);
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

        public bool Delete(User obj)
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

        public List<User> GetAll()
        {
            List<User> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from u in entities.Users select u;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<User>();

                    var user = new User
                    {
                        id = row.id,
                        email = row.email,
                        password = row.password,
                        firstname = row.firstname,
                        lastname = row.lastname,
                        about_me = row.about_me,
                        postal_code = row.postal_code,
                        avatar = row.avatar,
                        birthday = row.birthday,
                        country_id = row.country_id,
                        state_id = row.state_id,
                        district_id = row.district_id,
                        role1 = row.role1,
                        role2 = row.role2,
                        created_date = row.created_date
                    };

                    list.Add(user);
                }
            }

            return list;
        }

        public User GetById(int id)
        {
            User user = null;

            using (var entities = new TheProjectEntities())
            {
                var row = (from u in entities.Users where u.id == id select u).FirstOrDefault();

                if (row != null)
                    user = new User
                    {
                        id = row.id,
                        email = row.email,
                        password = row.password,
                        firstname = row.firstname,
                        lastname = row.lastname,
                        about_me = row.about_me,
                        postal_code = row.postal_code,
                        avatar = row.avatar,
                        birthday = row.birthday,
                        country_id = row.country_id,
                        state_id = row.state_id,
                        district_id = row.district_id,
                        role1 = row.role1,
                        role2 = row.role2,
                        created_date = row.created_date
                    };
            }

            return user;
        }

        public User Login(string email, string password)
        {
            User user = null;

            using (var entities = new TheProjectEntities())
            {
                var row = (from u in entities.Users where u.email == email && u.password == password select u)
                    .FirstOrDefault();

                if (row != null)
                    user = new User
                    {
                        id = row.id,
                        email = row.email,
                        firstname = row.firstname,
                        lastname = row.lastname,
                        about_me = row.about_me,
                        postal_code = row.postal_code,
                        avatar = row.avatar,
                        birthday = row.birthday,
                        country_id = row.country_id,
                        state_id = row.state_id,
                        district_id = row.district_id,
                        role1 = row.role1,
                        role2 = row.role2,
                        created_date = row.created_date
                    };
            }

            return user;
        }

        #endregion
    }
}