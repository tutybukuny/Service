namespace CoreServiceLib.Models
{
    public class District
    {
        public int id { get; set; }
        public string name { get; set; }
        public int state_id { get; set; }

        public bool MyEquals(object obj)
        {
            var d = (District) obj;

            return id == d.id && name == d.name && state_id == d.state_id;
        }
    }
}