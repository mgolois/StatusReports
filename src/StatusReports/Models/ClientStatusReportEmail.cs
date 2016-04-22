using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Models
{
    public class ClientStatusReportEmail
    {
        public int Id { get; set; }
        public int ClientStatusReportId { get; set; }
        public ClientStatusReport ClientStatusReport { get; set; }

        public string EmailAddress { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime? DateRead { get; set; }
        public bool Error { get; set; }
    }
}
