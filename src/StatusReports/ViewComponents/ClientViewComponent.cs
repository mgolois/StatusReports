using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StatusReports.Data;
using StatusReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace StatusReports.ViewComponents
//{
//    [ViewComponent(Name = "Clients")]
//    public class ClientViewComponent : ViewComponent
//    {
//        private IRepository<Client> statusReportRepo;


//        public ClientViewComponent(IRepository<Client> statusReportRepo)
//        {
//            this.statusReportRepo = statusReportRepo;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            return View(await GetClients());
//        }
//        private async Task<List<Client>> GetClients()
//        {
//            return await statusReportRepo.GetAll().ToListAsync();
//        }
//    }
//}
