using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StatusReports.Models;
using System.Threading.Tasks;
using System;

namespace StatusReports.Controllers
{
    public class StatusReportController : Controller
    {
        private StatusReportsDbContext _context;

        public StatusReportController(StatusReportsDbContext context)
        {
            _context = context;
        }

        // GET: IndividualStatus
        public async Task<IActionResult> Index()
        {
            var draftStatusReports = await _context.IndividualStatusReports.Include(i => i.Person)
                                                                         .Include(i => i.Project).ThenInclude(c => c.Client)
                                                                         .Include(i => i.Week)
                                                                         .Include(i => i.IndividualStatusItems)
                                                                         .ToListAsync();
            return View(draftStatusReports);
        }

        public IActionResult Submitted()
        {
            var submittedStatusReports = _context.IndividualStatusReports.Include(i => i.Person)
                                                                         .Include(i => i.Project)
                                                                         .Include(i => i.Week)
                                                                         .Where(i => i.Status == StatusCode.Submitted);
            return View(submittedStatusReports.ToList());
        }
        public IActionResult Approved()
        {
            var approvedStatusReports = _context.IndividualStatusReports.Include(i => i.Person)
                                                                         .Include(i => i.Project)
                                                                         .Include(i => i.Week)
                                                                         .Where(i => i.Status == StatusCode.Approved);
            return View(approvedStatusReports.ToList());
        }


        // GET: IndividualStatus/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var details = _context.IndividualStatusItems.Where(i => i.IndividualStatusReportId == id);
            //IndividualStatusReport individualStatusReport = _context.IndividualStatusReports.Single(m => m.Id == id);
            if (details == null)
            {
                return HttpNotFound();
            }

            return View(details.ToList());
        }

        // GET: IndividualStatus/Create
        public IActionResult Create()
        {
            //TODO: refactor this to pass in the model instead
            ViewData["PersonId"] = new SelectList(_context.People.ToList(), "PersonId", "FullName");
            ViewData["ProjectId"] = new SelectList(_context.Projects.ToList(), "Id", "Name");
            ViewData["WeekId"] = new SelectList(_context.Weeks.ToList(), "Id", "EndingDate");
            return View();
        }

        // POST: IndividualStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IndividualStatusReport individualStatusReport)
        {
            if (ModelState.IsValid)
            {
                //TODO: refactor to void performance hit by re-getting the date
                var weekSelected = _context.Weeks.FirstOrDefault(c => c.Id == individualStatusReport.WeekId);
                for (int i = 0; i < individualStatusReport.IndividualStatusItems.Count; i++)
                {
                    var daysToSubtract = new TimeSpan(6 - i, 0, 0, 0);
                    individualStatusReport.IndividualStatusItems[i].Date = weekSelected.EndingDate.Subtract(daysToSubtract);
                }

                //TODO: this status should be pass as argument, 
                //the user can create a status report and directly submit it to PM
                individualStatusReport.Status = StatusCode.Draft;

                _context.IndividualStatusReports.Add(individualStatusReport);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                //TODO: in case of invalid state, alert the user
            }

            //TODO: pass in a model instead TBD
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "FullName", individualStatusReport.PersonId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", individualStatusReport.ProjectId);
            ViewData["WeekId"] = new SelectList(_context.Weeks, "Id", "EndingDate", individualStatusReport.WeekId);
            return View(individualStatusReport);
        }

        // GET: IndividualStatus/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            IndividualStatusReport individualStatusReport = _context.IndividualStatusReports.Include(c => c.IndividualStatusItems)
                                                                                            .Include(x => x.Person)
                                                                                            .Include(x => x.Week)
                                                                                            .Include(x => x.Project)
                                                                                            .FirstOrDefault(m => m.Id == id);
            if (individualStatusReport == null)
            {
                return HttpNotFound();
            }

            //TODO: to be revised
            individualStatusReport.IndividualStatusItems = individualStatusReport.IndividualStatusItems.OrderBy(c => c.Date).ToList();

            //TODO: to be refactored
            ViewData["PersonId"] = individualStatusReport.PersonId;
            ViewData["ProjectId"] = individualStatusReport.ProjectId;
            ViewData["WeekId"] = individualStatusReport.Week.EndingDate;
            return View(individualStatusReport);
        }

        // POST: IndividualStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndividualStatusReport individualStatusReport)
        {
            if (ModelState.IsValid)
            {
                _context.Update(individualStatusReport);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["PersonId"] = new SelectList(_context.People.ToList(), "PersonId", "FullName");
            ViewData["ProjectId"] = new SelectList(_context.Projects.ToList(), "Id", "Name");
            ViewData["WeekId"] = new SelectList(_context.Weeks.ToList(), "Id", "EndingDate");
            return View(individualStatusReport);
        }

        // GET: IndividualStatus/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            IndividualStatusReport individualStatusReport = _context.IndividualStatusReports.Single(m => m.Id == id);
            if (individualStatusReport == null)
            {
                return HttpNotFound();
            }

            return View(individualStatusReport);
        }

        // POST: IndividualStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            IndividualStatusReport individualStatusReport = _context.IndividualStatusReports.Single(m => m.Id == id);
            _context.IndividualStatusReports.Remove(individualStatusReport);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
