using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;

namespace StatusReports.Models
{
    public class StatusReportsDbContext : DbContext
    {
        public StatusReportsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<ClientStatusReport> ClientStatusReports { get; set; }
        public DbSet<IndividualStatusReport> IndividualStatusReports { get; set; }
        public DbSet<IndividualStatusItem> IndividualStatusItems { get; set; }
        public DbSet<ClientStatusReportEmail> ClientStatusReportEmails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure the context if it hasn't been configured before
            // In the case of testing, we don't want to force it to use SQL Server
            if (!optionsBuilder.IsConfigured)
            {
                string connection = Startup.Configuration["Database:Connection"];
                optionsBuilder.UseSqlServer(connection);
            }
        }
    }
}
