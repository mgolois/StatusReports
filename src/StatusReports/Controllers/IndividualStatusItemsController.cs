using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StatusReports.Models;

namespace StatusReports.Controllers
{
    public class IndividualStatusItemsController : Controller
    {
        private StatusReportsDbContext _context;

        public IndividualStatusItemsController(StatusReportsDbContext context)
        {
            _context = context;    
        }

        // GET: IndividualStatusItems
        public IActionResult Index()
        {
            var statusReportsDbContext = _context.IndividualStatusItems.Include(i => i.StatusReport);
            return View(statusReportsDbContext.ToList());
        }

        // GET: IndividualStatusItems/Details/5
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

        // GET: IndividualStatusItems/Create
        public IActionResult Create()
        {
            //ViewData["IndividualStatusReportId"] = new SelectList(_context.IndividualStatusReports, "Id", "StatusReport");
            ViewData["IndividualStatusReportId"] = new SelectList(_context.IndividualStatusItems, "IndividualStatusReportId", "IndividualStatusReportId");
            return View();
        }

        // POST: IndividualStatusItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IndividualStatusItem individualStatusItem)
        {
            if (ModelState.IsValid)
            {
                _context.IndividualStatusItems.Add(individualStatusItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["IndividualStatusReportId"] = new SelectList(_context.IndividualStatusReports, "Id", "StatusReport", individualStatusItem.IndividualStatusReportId);
            return View(individualStatusItem);
        }

        // GET: IndividualStatusItems/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            IndividualStatusItem individualStatusItem = _context.IndividualStatusItems.Single(m => m.Id == id);
            if (individualStatusItem == null)
            {
                return HttpNotFound();
            }
            ViewData["IndividualStatusReportId"] = new SelectList(_context.IndividualStatusReports, "Id", "StatusReport", individualStatusItem.IndividualStatusReportId);
            return View(individualStatusItem);
        }

        // POST: IndividualStatusItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndividualStatusItem individualStatusItem)
        {
            //string reportId = Request.QueryString["IndividualStatusReportId"];
            string reportId = Request.QueryString.Value;
            if (ModelState.IsValid)
            {
                _context.Update(individualStatusItem);
                _context.SaveChanges();
                return RedirectToAction("Details", new {id = individualStatusItem.IndividualStatusReportId });
            }
            ViewData["IndividualStatusReportId"] = new SelectList(_context.IndividualStatusReports, "Id", "StatusReport", individualStatusItem.IndividualStatusReportId);
            return View(individualStatusItem);
        }
        public IActionResult Submit(int? id)
        {
            IndividualStatusItem individualStatusItem = _context.IndividualStatusItems.Single(m => m.Id == id);
            return RedirectToAction("Index", "IndividualStatus");
        }
        // GET: IndividualStatusItems/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            IndividualStatusItem individualStatusItem = _context.IndividualStatusItems.Single(m => m.Id == id);
            if (individualStatusItem == null)
            {
                return HttpNotFound();
            }

            return View(individualStatusItem);
        }

        // POST: IndividualStatusItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            IndividualStatusItem individualStatusItem = _context.IndividualStatusItems.Single(m => m.Id == id);
            _context.IndividualStatusItems.Remove(individualStatusItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
