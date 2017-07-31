using System.Collections.Generic;
using System.Data;
using CoreServiceLib.Models;

namespace CoreServiceLib.DAO
{
    public class StateDao : AbstractDao<State>
    {
        public StateDao(string connectStr)
            : base(connectStr)
        {
        }

        #region insert update delete

        public override bool Insert(State obj)
        {
            OpenConnection();

            var sql = "INSERT INTO State (name, country_id) VALUES(@name, @id)";
            var paramNames = new List<string> {"@name", "@id"};
            var dbTypes = new List<DbType> {DbType.String, DbType.Int32};
            var values = new List<object> {obj.name, obj.country_id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override bool Delete(State obj)
        {
            OpenConnection();

            var sql = "DELETE FROM State WHERE id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {obj.id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override bool Update(State obj)
        {
            OpenConnection();

            var sql = "UPDATE State SET name=@name, country_id=@country_id WHERE id=@id";
            var paramNames = new List<string> {"@name", "@country_id", "@id"};
            var dbTypes = new List<DbType> {DbType.String, DbType.Int32, DbType.Int32};
            var values = new List<object> {obj.name, obj.country_id, obj.id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        #endregion

        #region get item

        public override List<State> GetAll()
        {
            OpenConnection();

            var states = new List<State>();
            var sql = "SELECT * FROM State ORDER BY id ASC";
            var cmd = CreateCommand(sql);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
                states.Add(new State
                {
                    id = (int) reader["id"],
                    name = reader["name"].ToString(),
                    country_id = (int) reader["country_id"]
                });

            CloseConnection();

            return states;
        }

        public override State GetById(int id)
        {
            OpenConnection();

            State state = null;
            var sql = "SELECT * FROM State Where id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {id};
            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
                state = new State
                {
                    id = (int) reader["id"],
                    name = reader["name"].ToString(),
                    country_id = (int) reader["country_id"]
                };

            CloseConnection();

            return state;
        }

        #endregion
    }
}