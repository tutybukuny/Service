using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataTier.Dao
{
    public class TokenDao : IDao<Token>
    {
        #region Inset Update Delete

        public bool Insert(Token obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Tokens.Add(obj);
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Update(Token obj)
        {
            using (var entities = new TheProjectEntities())
            {
                try
                {
                    entities.Tokens.Attach(obj);
                    var entry = entities.Entry(obj);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.id).IsModified = false;
                    entry.Property(e => e.token).IsModified = false;

                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Delete(Token obj)
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

        public List<Token> GetAll()
        {
            List<Token> list = null;

            using (var entities = new TheProjectEntities())
            {
                var rows = from t in entities.Tokens select t;

                foreach (var row in rows)
                {
                    if (list == null) list = new List<Token>();

                    var token = new Token
                    {
                        id = row.id,
                        token = row.token,
                        user_id = row.user_id,
                        created_date = row.created_date
                    };

                    list.Add(token);
                }
            }

            return list;
        }

        public Token GetById(int? id)
        {
            Token token = null;

            using (var entities = new TheProjectEntities())
            {
                var row = (from t in entities.Tokens where t.id == id select t).FirstOrDefault();

                if (row != null)
                    token = new Token
                    {
                        id = row.id,
                        token = row.token,
                        user_id = row.user_id,
                        created_date = row.created_date
                    };
            }

            return token;
        }

        public int GetUserId(string token)
        {
            var user_id = -1;

            using (var entities = new TheProjectEntities())
            {
                var row = (from t in entities.Tokens
                    where t.token == token && DbFunctions.DiffHours(t.created_date, DateTime.Now) <= 3
                    select t).FirstOrDefault();

                if (row != null)
                {
                    var span = DateTime.Now.Subtract(row.created_date);
                    if (span.Hours <= 3) user_id = row.user_id;
                }
            }

            return user_id;
        }

        #endregion
    }
}