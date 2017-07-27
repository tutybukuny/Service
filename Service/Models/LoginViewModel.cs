using CoreServiceLib.Models;

namespace Service.Models
{
    public class LoginViewModel
    {
        public User User { get; set; }
        public bool Remember { get; set; }
    }
}