namespace Service.Models
{
    public class JsonUser
    {
        public string token { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int postal_code { get; set; }
        public int country_id { get; set; }
        public int state_id { get; set; }
        public int district_id { get; set; }
        public string avatar { get; set; }
    }
}