using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StatusReports.Models;
using System.Threading.Tasks;
using System;
using StatusReports.Data;
using System.Collections.Generic;
using StatusReports.ViewModels;

namespace StatusReports.Controllers
{
    public class StatusReportController : Controller
    {
        private IRepository<IndividualStatusReport> statusReportRepo;

        public StatusReportController(IRepository<IndividualStatusReport> statusReportRepo)
        {
            this.statusReportRepo = statusReportRepo;
        }

        // GET: IndividualStatus
        public async Task<IActionResult> Index()
        {
            var lookupData = await statusReportRepo.GetLookupDataAsync();
            return View(new StatusReportViewModel(null, lookupData));
        }

        // GET: IndividualStatus/Create
        public async Task<IActionResult> Create()
        {
            var lookupData = await statusReportRepo.GetLookupDataAsync();
            return View(new StatusReportViewModel(null, lookupData));
        }

        // POST: IndividualStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatusReportViewModel StatusReportVM, string save, string completed)
        {
            var lookupData = await statusReportRepo.GetLookupDataAsync();
            if (ModelState.IsValid)
            {
                //TODO: refactor to void performance hit by re-getting the date
                var weekSelected = DateTime.Parse(lookupData.Weeks.FirstOrDefault(c => c.LookupId == StatusReportVM.StatusReport.WeekId).LookupValue);

                for (int i = 0; i < StatusReportVM.StatusReport.IndividualStatusItems.Count; i++)
                {
                    var daysToSubtract = new TimeSpan(6 - i, 0, 0, 0);
                    StatusReportVM.StatusReport.IndividualStatusItems[i].Date = weekSelected.Subtract(daysToSubtract);
                }

                //the user can create a status report and directly submit it to PM
                if (!string.IsNullOrEmpty(save))
                {
                    StatusReportVM.StatusReport.Status = StatusCode.Draft;
                }
                if (!string.IsNullOrEmpty(completed))
                {
                    StatusReportVM.StatusReport.Status = StatusCode.Submitted;
                }

                await statusReportRepo.AddAndSaveAsync(StatusReportVM.StatusReport);
                return RedirectToAction("Index");
            }
            else
            {
                //TODO: in case of invalid state, alert the user
            }

            return View(new StatusReportViewModel(StatusReportVM.StatusReport, lookupData));
        }

        // GET: IndividualStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var statusRprt = await statusReportRepo.GetByIdAsync(id.Value);
            if (statusRprt == null)
            {
                return HttpNotFound();
            }

            //TODO: to be revised
            statusRprt.IndividualStatusItems = statusRprt.IndividualStatusItems.OrderBy(c => c.Date).ToList();

            return View(statusRprt);
        }

        // POST: IndividualStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndividualStatusReport individualStatusReport, string save, string completed)
        {
            if (ModelState.IsValid)
            {
                //Save the changes
                if (!string.IsNullOrEmpty(save))
                {
                    individualStatusReport.Status = StatusCode.Draft;
                }
                if (!string.IsNullOrEmpty(completed))
                {
                    individualStatusReport.Status = StatusCode.Submitted;
                }

                //Update the database
                statusReportRepo.UpdateAndSaveAsync(individualStatusReport);
                return RedirectToAction("Index");
            }

            return View(individualStatusReport);
        }

    }
}
