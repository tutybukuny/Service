//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataTier
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public int category_id { get; set; }
        public string description { get; set; }
        public int user_id { get; set; }
        public Nullable<int> country_id { get; set; }
        public Nullable<int> state_id { get; set; }
        public Nullable<int> district_id { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public bool completed { get; set; }
    }
}
