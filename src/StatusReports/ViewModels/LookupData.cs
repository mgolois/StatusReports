using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.ViewModels
{
    //public class LookupData
    //{
    //    public List<Person> People { get; set; }
    //    public List<Week> Weeks { get; set; }
    //    public List<Project> Projects { get; set; }
    //}
    public class LookupModel
    {
        public List<LookupItem> People { get; set; } 
        public List<LookupItem> Weeks { get; set; }
        public List<LookupItem> Projects { get; set; }
        public List<LookupItem> Clients { get; set; }
    }

    public class LookupItem
    {
        public int LookupId { get; set; }
        public string LookupValue { get; set; }
    }
    
}
