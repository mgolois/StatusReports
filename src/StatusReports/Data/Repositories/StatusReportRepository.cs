using Microsoft.Data.Entity;
using StatusReports.Models;
using StatusReports.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Data
{
    public class StatusReportRepository : IRepository<IndividualStatusReport>, IDisposable
    {
        private StatusReportsDbContext _context;

        public StatusReportRepository(StatusReportsDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(IndividualStatusReport statusReport)
        {
            _context.IndividualStatusReports.Add(statusReport);
        }

        public async Task<IndividualStatusReport> AddAndSaveAsync(IndividualStatusReport statusReport)
        {
            Add(statusReport);
            await SaveAsync();
            return statusReport;
        }

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }

        public IQueryable<IndividualStatusReport> GetAll()
        {
            return _context.IndividualStatusReports.AsQueryable();
        }

        public async Task<IndividualStatusReport> GetByIdAsync(int id)
        {
            return await _context.IndividualStatusReports.Include(c => c.IndividualStatusItems)
                                                         .Include(x => x.Person)
                                                         .Include(x => x.Week)
                                                         .Include(x => x.Project).FirstOrDefaultAsync(s => s.Id == id);
        }

       

        public async Task<LookupModel> GetLookupDataAsync()
        {
            var persontask = _context.People.Select(c=> new LookupItem { LookupId = c.PersonId, LookupValue = c.FullName }).ToListAsync();
            var projectTask = _context.Projects.Select(c => new LookupItem { LookupId = c.Id, LookupValue = c.Name }).ToListAsync();
            var weekTask = _context.Weeks.Select(c => new LookupItem { LookupId = c.Id, LookupValue = c.EndingDate.ToString("MM/dd/yyy") }).ToListAsync();
            var ClientTask = _context.Clients.Select(c => new LookupItem { LookupId = c.Id, LookupValue = c.Name }).ToListAsync();


            await Task.WhenAll(persontask, projectTask, weekTask,ClientTask);

            return new LookupModel
            {
                People = persontask.Result,
                Projects = projectTask.Result,
                Weeks = weekTask.Result,
                Clients = ClientTask.Result
            };
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update(IndividualStatusReport statusReport)
        {
            _context.IndividualStatusReports.Update(statusReport);
        }

        public async Task<IndividualStatusReport> UpdateAndSaveAsync(IndividualStatusReport statusReport)
        {
            Update(statusReport);
            await SaveAsync();
            return statusReport;
        }

        public IQueryable<Client> GetClients()
        {
            return _context.Clients.AsQueryable();
        }
    }
}
