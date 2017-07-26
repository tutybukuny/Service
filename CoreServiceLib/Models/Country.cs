namespace CoreServiceLib.Models
{
    public class Country
    {
        public int id { get; set; }
        public string name { get; set; }

        public bool MyEquals(object obj)
        {
            var country = (Country) obj;
            return id == country.id && name == country.name;
        }
    }
}