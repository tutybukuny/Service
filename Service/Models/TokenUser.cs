using DataTier;

namespace Service.Models
{
    public class TokenUser
    {
        public User user { get; set; }
        public string token { get; set; }
    }
}