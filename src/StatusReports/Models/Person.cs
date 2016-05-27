using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusReports.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        public int RdaEmployeeId { get; set; }

        public string PhotoUrl { get; set; }

        public string UserName { get; set; }

        [Required, MaxLength(127)]
        public string FirstName { get; set; }

        [Required, MaxLength(127)]
        public string LastName { get; set; }

        public bool Active { get; set; }
        public DateTime? DeactivateDate { get; set; }
        
        public virtual ICollection<IndividualStatusReport> Reports { get; set; }

        public List<IndividualStatusReport> IndividualStatusReports { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
