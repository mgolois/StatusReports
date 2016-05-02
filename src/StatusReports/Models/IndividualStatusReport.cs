using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Models
{
    public class IndividualStatusReport
    {
        public int Id { get; set; }

        public int WeekId { get; set; }
        public Week Week { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

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
