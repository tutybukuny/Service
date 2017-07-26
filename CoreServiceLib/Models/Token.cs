using System;

namespace CoreServiceLib.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string TokenString { get; set; }
        public User User { get; set; }
        public DateTime DateTime { get; set; }

        public bool MyEquals(Token token)
        {
            return Id == token.Id && TokenString == token.TokenString && User.MyEquals(token.User) &&
                   DateTime == token.DateTime;
        }
    }
}