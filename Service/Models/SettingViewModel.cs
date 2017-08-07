using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTier;

namespace Service.Models
{
    public class SettingViewModel
    {
        public User User { get; set; }
        public List<Country> Countries { get; set; }
        public List<State> States { get; set; }
        public List<District> Districts { get; set; }
    }
}