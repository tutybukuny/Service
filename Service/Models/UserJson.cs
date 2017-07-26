using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class UserJson
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string chat_status { get; set; }
        public string biography { get; set; }
        public int portal_code { get; set; }
        public int country_id { get; set; }
        public int state_id { get; set; }
        public int district_id { get; set; }
        public string avatarUrl { get; set; }
    }
}