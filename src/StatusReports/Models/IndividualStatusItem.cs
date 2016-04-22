using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Models
{
    public class IndividualStatusItem
    {
        public int Id { get; set; }
        public Decimal Hours { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Work")]
        public string WorkDescription { get; set; }
        public DateTime Date { get; set; }

        public int IndividualStatusReportId { get; set; }
        public IndividualStatusReport StatusReport { get; set; }
    }
}
