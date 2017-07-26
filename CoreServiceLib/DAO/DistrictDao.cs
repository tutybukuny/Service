﻿using System.Collections.Generic;
using System.Data;
using CoreServiceLib.Models;

namespace CoreServiceLib.DAO
{
    public class DistrictDao : AbstractDao<District>
    {
        public DistrictDao(string connectStr)
            : base(connectStr)
        {
        }

        #region insert update delete

        public override bool Insert(District obj)
        {
            OpenConnection();

            var sql = "INSERT INTO tbl_districts (name, state_id) VALUES(@name, @id)";
            var paramNames = new List<string> {"@name", "@id"};
            var dbTypes = new List<DbType> {DbType.String, DbType.Int32};
            var values = new List<object> {obj.name, obj.state_id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override bool Delete(District obj)
        {
            OpenConnection();

            var sql = "DELETE FROM tbl_districts WHERE id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {obj.id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public override bool Update(District obj)
        {
            OpenConnection();

            var sql = "UPDATE tbl_districts SET name=@name, state_id=@state_id WHERE id=@id";
            var paramNames = new List<string> {"@name", "@state_id", "@id"};
            var dbTypes = new List<DbType> {DbType.String, DbType.Int32, DbType.Int32};
            var values = new List<object> {obj.name, obj.state_id, obj.id};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        #endregion

        #region get item

        public override List<District> GetAll()
        {
            OpenConnection();

            var districts = new List<District>();
            var sql = "SELECT * FROM tbl_districts ORDER BY id ASC";
            var cmd = CreateCommand(sql);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
                districts.Add(new District
                {
                    id = (int) reader["id"],
                    name = reader["name"].ToString(),
                    state_id = (int) reader["state_id"]
                });

            CloseConnection();

            return districts;
        }

        public override District GetById(int id)
        {
            OpenConnection();

            District district = null;
            var sql = "SELECT * FROM tbl_districts Where id=@id";
            var paramNames = new List<string> {"@id"};
            var dbTypes = new List<DbType> {DbType.Int32};
            var values = new List<object> {id};
            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            var stateDao = new StateDao(connectStr);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
                district = new District
                {
                    id = (int) reader["id"],
                    name = reader["name"].ToString(),
                    state_id = (int) reader["state_id"]
                };

            CloseConnection();

            return district;
        }

        #endregion
    }
}