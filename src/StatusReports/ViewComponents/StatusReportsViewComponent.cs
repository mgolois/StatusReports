using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using StatusReports.Models;
using StatusReports.Data;
using Microsoft.Data.Entity;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StatusReports.ViewComponents
{
    [ViewComponent(Name = "StatusReports")]
    public class StatusReportsViewComponent : ViewComponent
    {
        private IRepository<IndividualStatusReport> statusReportRepo;

        public StatusReportsViewComponent(IRepository<IndividualStatusReport> statusReportRepo)
        {
            this.statusReportRepo = statusReportRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync(StatusCode status)
        {
            return View(await GetStatusReports(status));
        }

        private async Task<List<IndividualStatusReport>> GetStatusReports(StatusCode status)
        {
            return await statusReportRepo.GetAll().Include(i => i.Person)
                                                   .Include(i => i.Project).ThenInclude(c => c.Client)
                                                   .Include(i => i.Week)
                                                   .Include(i => i.IndividualStatusItems)
                                                   .Where(i => i.Status == status)
                                                   .ToListAsync();
        }
    }
}
