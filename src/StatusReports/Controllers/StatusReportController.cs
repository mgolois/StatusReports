using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StatusReports.Models;
using System.Threading.Tasks;
using System;
using StatusReports.Data;
using System.Collections.Generic;

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
        public IActionResult Index()
        {
            return View();
        }

        // GET: IndividualStatus/Create
        public async Task<IActionResult> Create()
        {
            //TODO: refactor this to pass in the model instead

            var lookupData = await statusReportRepo.GetLookupData();
            ViewData["PersonId"] = new SelectList(lookupData.People, "PersonId", "FullName");
            ViewData["ProjectId"] = new SelectList(lookupData.Projects, "Id", "Name");
            ViewData["WeekId"] = new SelectList(lookupData.Weeks, "Id", "EndingDate");

            return View();
        }

        // POST: IndividualStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IndividualStatusReport individualStatusReport, string save, string completed)
        {
            var lookupData = await statusReportRepo.GetLookupData();
            if (ModelState.IsValid)
            {
                //TODO: refactor to void performance hit by re-getting the date
                var weekSelected = lookupData.Weeks.FirstOrDefault(c => c.Id == individualStatusReport.WeekId);

                for (int i = 0; i < individualStatusReport.IndividualStatusItems.Count; i++)
                {
                    var daysToSubtract = new TimeSpan(6 - i, 0, 0, 0);
                    individualStatusReport.IndividualStatusItems[i].Date = weekSelected.EndingDate.Subtract(daysToSubtract);
                }


                //the user can create a status report and directly submit it to PM
                if (!string.IsNullOrEmpty(save))
                {
                    individualStatusReport.Status = StatusCode.Draft;
                }
                if (!string.IsNullOrEmpty(completed))
                {
                    individualStatusReport.Status = StatusCode.Submitted;
                }

                await statusReportRepo.AddAndSaveAsync(individualStatusReport);
                return RedirectToAction("Index");
            }
            else
            {
                //TODO: in case of invalid state, alert the user
            }

            //TODO: pass in a model instead TBD
            ViewData["PersonId"] = new SelectList(lookupData.People, "PersonId", "FullName", individualStatusReport.PersonId);
            ViewData["ProjectId"] = new SelectList(lookupData.Projects, "Id", "Name", individualStatusReport.ProjectId);
            ViewData["WeekId"] = new SelectList(lookupData.Weeks, "Id", "EndingDate", individualStatusReport.WeekId);
            return View(individualStatusReport);
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
