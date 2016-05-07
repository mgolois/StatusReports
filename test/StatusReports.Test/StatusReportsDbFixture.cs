using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using StatusReports.Data;
using StatusReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Test
{
    public class StatusReportsDbFixture
    {
        public StatusReportsDbContext Context { get; set; }
        public StatusReportsDbFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StatusReportsDbContext>();
            optionsBuilder.UseInMemoryDatabase();
            DbContextOptions<StatusReportsDbContext> contextOptions = optionsBuilder.Options;

            Context = new StatusReportsDbContext(contextOptions);
        }

        public void InitializeDatabase()
        {
            SampleData.Initialize(Context);
        }
    }
}
