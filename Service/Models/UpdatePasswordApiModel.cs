namespace Service.Models
{
    public class UpdatePasswordApiModel
    {
        public int? user_id { get; set; }
        public string old_password { get; set; }
        public string new_password { get; set; }
    }
}