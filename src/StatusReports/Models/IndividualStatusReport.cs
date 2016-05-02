using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Models
{
    public class IndividualStatusReport
    {
        public int Id { get; set; }
        [Display(Name = "Week Ending On")]
        public int WeekId { get; set; }
        public Week Week { get; set; }
        [Display(Name= "Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }
        [Display(Name = "Project Name")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public StatusCode Status { get; set; }

        public List<IndividualStatusItem> IndividualStatusItems { get; set; }
    }

    public enum StatusCode
    {
        Draft, Submitted, Approved
    }

}
