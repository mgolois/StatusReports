using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Models
{
    public class ClientStatusReport
    {
        public int Id { get; set; }
        public int WeekId { get; set; }
        public Week Week { get; set; }

        public string StatusSummary { get; set; }

        public ICollection<ClientStatusReportEmail> Emails { get; set; }

        public ICollection<IndividualStatusReport> IndividualStatusReports { get; set; }

    }
}
