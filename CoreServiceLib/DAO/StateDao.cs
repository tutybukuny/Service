using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CoreServiceLib.Models;

namespace CoreServiceLib.DAO
{
    public class StateDao : AbstractDao<State>
    {
        public StateDao(string connectStr) : base(connectStr)
        {
        }

        public override bool Insert(State obj)
        {
            OpenConnection();

            var sql = "INSERT INTO tbl_states (name, country_id) VALUES(@name, @id)";
            var paramNames = new List<string> {"@name", "@id"};
            var dbTypes = new List<DbType> {DbType.String, DbType.Int32};
            var values = new List<object> {obj.Name, obj.Country.Id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override bool Delete(State obj)
        {
            OpenConnection();

            var sql = "DELETE FROM tbl_states WHERE id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {obj.Id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override bool Update(State obj)
        {
            OpenConnection();

            var sql = "UPDATE tbl_states SET name=@name, country_id=@country_id WHERE id=@id";
            var paramNames = new List<string> {"@name", "@country_id", "@id"};
            var dbTypes = new List<DbType> {DbType.String, DbType.Int32, DbType.Int32};
            var values = new List<object> {obj.Name, obj.Country.Id, obj.Id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override List<State> GetAll()
        {
            OpenConnection();

            var states = new List<State>();
            var sql = "SELECT * FROM tbl_states ORDER BY id ASC";
            var cmd = CreateCommand(sql);

            var reader = cmd.ExecuteReader();
            var countryDao = new CountryDao(connectStr);

            while (reader.Read())
                states.Add(new State
                {
                    Id = (int) reader["id"],
                    Name = reader["name"].ToString(),
                    Country = countryDao.GetById((int) reader["country_id"])
                });

            CloseConnection();

            return states;
        }

        public override State GetById(int id)
        {
            OpenConnection();

            State state = null;
            var sql = "SELECT * FROM tbl_states Where id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {id};
            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            var countryDao = new CountryDao(connectStr);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
                state = new State
                {
                    Id = (int) reader["id"],
                    Name = reader["name"].ToString(),
                    Country = countryDao.GetById((int) reader["country_id"])
                };

            CloseConnection();

            return state;
        }
    }
}