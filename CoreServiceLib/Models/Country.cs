namespace CoreServiceLib.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool MyEquals(object obj)
        {
            var country = (Country) obj;
            return Id == country.Id && Name == country.Name;
        }
    }
}