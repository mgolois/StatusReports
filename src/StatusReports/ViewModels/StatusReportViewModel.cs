using StatusReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.ViewModels
{
    public class StatusReportViewModel
    {
        public StatusReportViewModel()
        { }
        public StatusReportViewModel(IndividualStatusReport statusReport, LookupModel lookupData)
        {
            StatusReport = statusReport;
            LookupData = lookupData;
        }
        public IndividualStatusReport StatusReport { get; set; }
        public LookupModel LookupData { get; set; }
    }
}
