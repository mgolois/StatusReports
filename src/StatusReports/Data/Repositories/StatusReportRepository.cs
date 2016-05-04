using Microsoft.Data.Entity;
using StatusReports.Models;
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

        public async Task<LookupData> GetLookupData()
        {
            var persontask = _context.People.ToListAsync();
            var projectTask = _context.Projects.ToListAsync();
            var weekTask = _context.Weeks.ToListAsync();

            await Task.WhenAll(persontask, projectTask, weekTask);

            return new LookupData
            {
                People = persontask.Result,
                Projects = projectTask.Result,
                Weeks = weekTask.Result
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


    }
}
