using System.Collections.Generic;

namespace CoreServiceLib.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int postal_code { get; set; }
        public int country_id { get; set; }
        public int state_id { get; set; }
        public int district_id { get; set; }
        public string avatar { get; set; }
        public string token { get; set; }

        public bool MyEquals(object obj)
        {
            var u = (User) obj;
            return id == u.id && firstname == u.firstname && lastname == u.lastname && postal_code == u.postal_code &&
                   country_id == u.country_id && state_id == u.state_id && district_id == u.district_id &&
                   avatar == u.avatar;
        }

        public List<object> PropsToList()
        {
            var type = GetType();
            var props = type.GetProperties();
            var list = new List<object>();

            foreach (var prop in props)
            {
                var value = prop.GetValue(this, null);
                list.Add(value);
            }

            list.RemoveAt(0);
            list.RemoveAt(list.Count - 1);

            return list;
        }
    }
}