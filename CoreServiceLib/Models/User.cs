using System.Collections.Generic;

namespace CoreServiceLib.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int PostalCode { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }
        public District District { get; set; }
        public string Avatar { get; set; }

        public bool MyEquals(object obj)
        {
            var u = (User) obj;
            return Id == u.Id && Firstname == u.Firstname && Lastname == u.Lastname && PostalCode == u.PostalCode &&
                   Country.MyEquals(u.Country) && State.MyEquals(u.State) && District.MyEquals(u.District) &&
                   Avatar == u.Avatar;
        }

        public List<object> PropsToList()
        {
            var type = GetType();
            var props = type.GetProperties();
            var list = new List<object>();

            foreach (var prop in props)
            {
                var value = prop.GetValue(this, null);
                if (value.GetType() == typeof(Country))
                {
                    var c = (Country) value;
                    list.Add(c.Id);
                }
                else if (value.GetType() == typeof(State))
                {
                    var s = (State) value;
                    list.Add(s.Id);
                }
                else if (value.GetType() == typeof(District))
                {
                    var d = (District) value;
                    list.Add(d.Id);
                }
                else
                {
                    list.Add(value);
                }
            }

            list.RemoveAt(0);

            return list;
        }
    }
}