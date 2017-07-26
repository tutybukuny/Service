namespace CoreServiceLib.Models
{
    public class State
    {
        public int id { get; set; }
        public string name { get; set; }
        public int country_id { get; set; }

        public bool MyEquals(object obj)
        {
            var s = (State) obj;
            return id == s.id && name == s.name && country_id == s.country_id;
        }
    }
}