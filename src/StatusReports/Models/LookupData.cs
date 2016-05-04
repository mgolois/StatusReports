using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Models
{
    public class LookupData
    {
        public List<Person> People { get; set; }
        public List<Week> Weeks { get; set; }
        public List<Project> Projects { get; set; }
    }
    /*public class LookupModel
    {
        public List<LookupItem<string>> People { get; set; } 
        public List<LookupItem<DateTime>> Weeks { get; set; }
        public List<LookupItem<string>> Projects { get; set; }
    }

    public class LookupItem<T>
    {
        public int LookupId { get; set; }
        public T LookupValue { get; set; }
    }
    */
}
