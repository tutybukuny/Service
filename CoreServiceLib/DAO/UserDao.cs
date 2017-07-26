using System.Collections.Generic;
using System.Data;
using CoreServiceLib.Models;

namespace CoreServiceLib.DAO
{
    public class UserDao : AbstractDao<User>
    {
        public UserDao(string connectStr) : base(connectStr)
        {
        }

        public override bool Insert(User obj)
        {
            OpenConnection();

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
            var values = obj.PropsToList();

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
            var values = new List<object> {obj.Id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override bool Update(User obj)
        {
            OpenConnection();

            var sql =
                "UPDATE tbl_users SET email=@email, password=@password, firstname=@firstname, lastname=@lastname, " +
                "postal_code=@postal_code, country_id=@country_id, state_id=@state_id, district_id=@district_id, avatar=@avatar WHERE id=@id";
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
                "@avatar",
                "@id"
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
                DbType.String,
                DbType.Int32
            };
            var values = obj.PropsToList();
            values.Add(obj.Id);

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override List<User> GetAll()
        {
            OpenConnection();
            var users = new List<User>();
            var sql = "SELECT * FROM tbl_users ORDER BY id ASC";
            var comd = CreateCommand(sql);
            var reader = comd.ExecuteReader();
            var dao = new DistrictDao(connectStr);

            while (reader.Read())
            {
                var district = dao.GetById((int) reader["district_id"]);
                var state = district.State;
                var country = state.Country;

                users.Add(new User
                {
                    Id = (int) reader["id"],
                    Firstname = reader["firstname"].ToString(),
                    Lastname = reader["lastname"].ToString(),
                    PostalCode = (int) reader["postal_code"],
                    Country = country,
                    State = state,
                    District = district,
                    Avatar = reader["avatar"].ToString()
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
            var dao = new DistrictDao(connectStr);

            if (reader.Read())
            {
                var district = dao.GetById((int) reader["district_id"]);
                var state = district.State;
                var country = state.Country;

                user = new User
                {
                    Id = (int) reader["id"],
                    Firstname = reader["firstname"].ToString(),
                    Lastname = reader["lastname"].ToString(),
                    PostalCode = (int) reader["postal_code"],
                    Country = country,
                    State = state,
                    District = district,
                    Avatar = reader["avatar"].ToString()
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
            var dao = new DistrictDao(connectStr);

            if (reader.Read())
            {
                var district = dao.GetById((int) reader["district_id"]);
                var state = district.State;
                var country = state.Country;

                user = new User
                {
                    Id = (int) reader["id"],
                    Firstname = reader["firstname"].ToString(),
                    Lastname = reader["lastname"].ToString(),
                    PostalCode = (int) reader["postal_code"],
                    Country = country,
                    State = state,
                    District = district,
                    Avatar = reader["avatar"].ToString()
                };
            }

            CloseConnection();

            return user;
        }
    }
}