using System.Collections.Generic;
using System.Data;
using CoreServiceLib.Models;

namespace CoreServiceLib.DAO
{
    public class CountryDao : AbstractDao<Country>
    {
        public CountryDao(string connectStr)
            : base(connectStr)
        {
        }

        public override bool Insert(Country obj)
        {
            OpenConnection();

            var sql = "INSERT INTO tbl_countries (name) VALUES(@name)";
            var paramNames = new List<string> {"@name"};
            var dbTypes = new List<DbType> {DbType.String};
            var values = new List<object> {obj.Name};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();
            return true;
        }

        public override bool Delete(Country obj)
        {
            OpenConnection();

            var sql = "DELETE FROM tbl_countries WHERE id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {obj.Id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }


        public override bool Update(Country obj)
        {
            OpenConnection();

            var sql = "UPDATE tbl_countries SET name=@name WHERE id=@id";
            var paramNames = new List<string> {"@id", "@name"};
            var dbTypes = new List<DbType> {DbType.Int32, DbType.String};
            var values = CreateValues(obj);

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override List<Country> GetAll()
        {
            OpenConnection();

            var countries = new List<Country>();
            var sql = "SELECT * FROM tbl_countries ORDER BY id ASC";
            var cmd = CreateCommand(sql);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
                countries.Add(new Country {Id = (int) reader["id"], Name = reader["name"].ToString()});

            CloseConnection();

            return countries;
        }

        public override Country GetById(int id)
        {
            OpenConnection();

            Country country = null;
            var sql = "SELECT * FROM tbl_countries Where id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {id};
            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
                country = new Country {Id = (int) reader["id"], Name = reader["name"].ToString()};

            CloseConnection();

            return country;
        }
    }
}