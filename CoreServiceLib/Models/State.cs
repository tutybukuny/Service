namespace CoreServiceLib.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }

        public bool MyEquals(object obj)
        {
            var s = (State) obj;
            return Id == s.Id && Name == s.Name && Country.MyEquals(s.Country);
        }
    }
}