using System.Collections.Generic;
using System.Data;
using CoreServiceLib.Models;

namespace CoreServiceLib.DAO
{
    public class UserDao : AbstractDao<User>
    {
        public UserDao(string connectStr)
            : base(connectStr)
        {
        }

        /// <summary>
        ///     Check email is exsist or not
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        public bool CheckExistsEmail(string email)
        {
            OpenConnection();

            var sql = "SELECT * FROM tbl_users WHERE email=@email";
            var paramNames = new List<string> {"@email"};
            var dbTypes = new List<DbType> {DbType.String};
            var values = new List<object> {email};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            var reader = cmd.ExecuteReader();

            var result = reader.Read();

            CloseConnection();

            return result;
        }

        #region insert update delete

        public override bool Insert(User obj)
        {
            OpenConnection();

            if (obj.country_id == 0)
                obj.country_id = 21;

            if (obj.state_id == 0)
                obj.state_id = 17;

            if (obj.district_id == 0)
                obj.district_id = 17;

            if (string.IsNullOrEmpty(obj.avatar))
                obj.avatar = "default";


            var sql =
                "INSERT INTO tbl_users (email, password, firstname, lastname, postal_code, country_id, state_id, district_id, avatar) " +
                "VALUES(@email, @password, @firstname, @lastname, @postal_code, @country_id, @state_id, @district_id, @avatar)";
            var paramNames = new List<string>
            {
                "@email",
                "@password",
                "@firstname",
                "@lastname",
                "@postal_code",
                "@country_id",
                "@state_id",
                "@district_id",
                "@avatar"
            };
            var dbTypes = new List<DbType>
            {
                DbType.String,
                DbType.String,
                DbType.String,
                DbType.String,
                DbType.Int32,
                DbType.Int32,
                DbType.Int32,
                DbType.Int32,
                DbType.String
            };

            var values = new List<object>
            {
                obj.email,
                obj.password,
                obj.firstname,
                obj.lastname,
                obj.postal_code,
                obj.country_id,
                obj.state_id,
                obj.district_id,
                obj.avatar
            };

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override bool Delete(User obj)
        {
            OpenConnection();

            var sql = "DELETE FROM tbl_users WHERE id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {obj.id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override bool Update(User obj)
        {
            OpenConnection();

            var sql =
                "UPDATE tbl_users SET firstname=@firstname, lastname=@lastname, " +
                "postal_code=@postal_code, country_id=@country_id, state_id=@state_id, district_id=@district_id, avatar=@avatar WHERE id=@id";
            var paramNames = new List<string>
            {
                "@firstname",
                "@lastname",
                "@postal_code",
                "@country_id",
                "@state_id",
                "@district_id",
                "@avatar",
                "@id"
            };
            var dbTypes = new List<DbType>
            {
                DbType.String,
                DbType.String,
                DbType.Int32,
                DbType.Int32,
                DbType.Int32,
                DbType.Int32,
                DbType.String,
                DbType.Int32
            };
            var values = new List<object>
            {
                obj.firstname,
                obj.lastname,
                obj.postal_code,
                obj.country_id,
                obj.state_id,
                obj.district_id,
                obj.avatar,
                obj.id
            };

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        #endregion

        #region get item

        public override List<User> GetAll()
        {
            OpenConnection();
            var users = new List<User>();
            var sql = "SELECT * FROM tbl_users ORDER BY id ASC";
            var comd = CreateCommand(sql);
            var reader = comd.ExecuteReader();
            var districtDao = new DistrictDao(connectStr);
            var stateDao = new StateDao(connectStr);
            var countryDao = new CountryDao(connectStr);

            while (reader.Read())
            {
                var district = districtDao.GetById((int) reader["district_id"]);
                var state = stateDao.GetById(district.state_id);
                var country = countryDao.GetById(state.country_id);

                users.Add(new User
                {
                    id = (int) reader["id"],
                    firstname = reader["firstname"].ToString(),
                    lastname = reader["lastname"].ToString(),
                    postal_code = (int) reader["postal_code"],
                    country_id = country.id,
                    state_id = state.id,
                    district_id = district.id,
                    avatar = reader["avatar"].ToString()
                });
            }

            CloseConnection();

            return users;
        }

        public override User GetById(int id)
        {
            OpenConnection();
            User user = null;
            var sql = "SELECT * FROM tbl_users WHERE id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {id};

            var comd = CreateCommand(sql, paramNames, dbTypes, values);
            var reader = comd.ExecuteReader();
            var districtDao = new DistrictDao(connectStr);
            var stateDao = new StateDao(connectStr);
            var countryDao = new CountryDao(connectStr);

            if (reader.Read())
            {
                var district = districtDao.GetById((int) reader["district_id"]);
                var state = stateDao.GetById(district.state_id);
                var country = countryDao.GetById(state.country_id);

                user = new User
                {
                    id = (int) reader["id"],
                    firstname = reader["firstname"].ToString(),
                    lastname = reader["lastname"].ToString(),
                    postal_code = (int) reader["postal_code"],
                    country_id = country.id,
                    state_id = state.id,
                    district_id = district.id,
                    avatar = reader["avatar"].ToString()
                };
            }

            CloseConnection();

            return user;
        }

        public User GetUserByLoginInfo(string email, string password)
        {
            OpenConnection();
            User user = null;
            var sql = "SELECT * FROM tbl_users WHERE email=@email AND password=@password";
            var paramNames = new List<string> {"@email", "@password"};
            var dbTypes = new List<DbType> {DbType.String, DbType.String};
            var values = new List<object> {email, password};

            var comd = CreateCommand(sql, paramNames, dbTypes, values);
            var reader = comd.ExecuteReader();
            var districtDao = new DistrictDao(connectStr);
            var stateDao = new StateDao(connectStr);
            var countryDao = new CountryDao(connectStr);

            if (reader.Read())
            {
                var district = districtDao.GetById((int) reader["district_id"]);
                var state = stateDao.GetById(district.state_id);
                var country = countryDao.GetById(state.country_id);

                user = new User
                {
                    id = (int) reader["id"],
                    firstname = reader["firstname"].ToString(),
                    lastname = reader["lastname"].ToString(),
                    postal_code = (int) reader["postal_code"],
                    country_id = country.id,
                    state_id = state.id,
                    district_id = district.id,
                    avatar = reader["avatar"].ToString()
                };
            }

            CloseConnection();

            return user;
        }

        #endregion
    }
}