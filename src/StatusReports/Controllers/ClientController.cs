using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StatusReports.Models;
using StatusReports.Data;
using StatusReports.ViewModels;
using System.Collections.Generic;

namespace StatusReports.Controllers
{
    public class ClientController : Controller
    {
        //private StatusReportsDbContext _db;
        private IRepository<Client> statusReportRepo;

        //public ClientController(StatusReportsDbContext db)
        //{
        //    _db = db;
        //}

        //  private StatusReportsDbContext _context;

        public ClientController(IRepository<Client> statusReportRepo)
        {
            this.statusReportRepo = statusReportRepo;
        }

        // GET: Client
        public IActionResult Index()
        {
            List<Client> clients = statusReportRepo.GetClients().ToList();

            //var lookupData = await statusReportRepo.GetClients();
            //List<LookupItem> p = lookupData.
            return View(clients);

            //var lookupData = await statusReportRepo.GetLookupDataAsync();
            //return View(new StatusReportViewModel(null, lookupData));
            //return View(new ClientViewModel(null, lookupData));
            //return View(statusReportRepo.GetClients().ToList());

            //List<Client> clients = await _db.Clients.ToListAsync();
            //List<Client> c = await _db.Clients.ToListAsync();
            // Client cs = c.First();
            //Project p =  _db.Projects.Where(n => n.Client.Name == cs.Name).FirstOrDefault();
            ////List<Project> p = cs.Projects;
            //ViewBag.projects = p;

            //return View(await _db.Clients.ToListAsync());
        }




    }
}
