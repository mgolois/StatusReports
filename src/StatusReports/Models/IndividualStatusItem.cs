using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatusReports.Models
{
    public class IndividualStatusItem
    {
        public int Id { get; set; }
        public Decimal Hours { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Work")]
        public string WorkDescription { get; set; }
        public DateTime? Date { get; set; }

        public int IndividualStatusReportId { get; set; }
        public IndividualStatusReport StatusReport { get; set; }

        [NotMapped]
        public string FormattedDate
        {
            get
            {
                return Date?.ToString("MM/dd/yyyy");
            }
        }

        [NotMapped]
        public string FormattedDay
        {
            get
            {
                return Date?.ToString("dddd");
            }
        }
    }
}
