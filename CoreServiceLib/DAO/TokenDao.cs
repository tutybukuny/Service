using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CoreServiceLib.Models;

namespace CoreServiceLib.DAO
{
    public class TokenDao : AbstractDao<Token>
    {
        public TokenDao(string connectStr) : base(connectStr)
        {
        }

        public bool CheckToken(string token)
        {
            OpenConnection();

            var sql = "SELECT * FROM tbl_token WHERE token=@token";
            var paramNames = new List<string> {"@token"};
            var dbTypes = new List<DbType> {DbType.String};
            var values = new List<object> {token};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            var reader = cmd.ExecuteReader();
            var result = false;

            if (reader.Read())
            {
                var dateTime = (DateTime) reader["created_date"];
                var span = DateTime.Now.Subtract(dateTime);

                if (span.Minutes <= 30) result = true;
            }

            CloseConnection();

            return result;
        }

        public override bool Insert(Token obj)
        {
            OpenConnection();

            var sql =
                "INSERT INTO tbl_token (token, user_id, created_date) VALUES(@token, @user_id, @created_date)";
            var paramNames = new List<string> {"@token", "@user_id", "@created_date"};
            var dbTypes = new List<DbType> {DbType.String, DbType.Int32, DbType.DateTime};
            var values = new List<object> {obj.TokenString, obj.User.Id, DateTime.Now};

            var cmd = CreateCommand(sql, paramNames, dbTypes, values);

            cmd.ExecuteNonQuery();

            CloseConnection();

            return true;
        }

        public string AutoGenerate()
        {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }

        public override bool Delete(Token obj)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Token obj)
        {
            throw new NotImplementedException();
        }

        public override List<Token> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Token GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}