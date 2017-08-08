using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using DataTier.Factory;

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
            try
            {
                var conn = new SqlConnection(DaoLib.ConnectionString);
                conn.Open();
                var paramNames = new List<string> {"@firstname", "@lastname", "@email", "@password", "@created_date"};
                var dbTypes = new List<DbType>
                {
                    DbType.String,
                    DbType.String,
                    DbType.String,
                    DbType.String,
                    DbType.DateTime
                };
                var values = new List<object> {obj.firstname, obj.lastname, obj.email, obj.password, obj.created_date};
                var sql = "INSERT INTO dbo.[User](firstname, lastname, email, password, created_date) " +
                          "VALUES(@firstname, @lastname, @email, @password, @created_date)";
                var cmd = DaoLib.CreateCommand(conn, sql, paramNames, dbTypes, values);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Update(User obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    var row = entities.Users.SingleOrDefault(u => u.id == obj.id);

                    if (row != null)
                    {
                        row.firstname = obj.firstname;
                        row.lastname = obj.lastname;
                        row.country_id = obj.country_id;
                        row.state_id = obj.state_id;
                        row.district_id = obj.district_id;
                        row.role1 = obj.role1;
                        row.role2 = obj.role2;
                    }

                    entities.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " +
                                          validationError.ErrorMessage);
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

        public User GetById(int? id)
        {
            User user = null;

            using (var entities = new TheProjectEntities())
            {
                var row = (from u in entities.Users where u.id == id select u).FirstOrDefault();

                if (row != null)
                    user = new User
                    {
                        id = row.id,
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

        public User GetByToken(string token)
        {
            var tokenDao = (TokenDao) DaoFactory.GetDao("TokenDao");
            var id = tokenDao.GetUserId(token);
            return GetById(id);
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