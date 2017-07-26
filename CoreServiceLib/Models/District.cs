namespace CoreServiceLib.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }

        public bool MyEquals(object obj)
        {
            var d = (District) obj;

            return Id == d.Id && Name == d.Name && State.MyEquals(d.State);
        }
    }
}