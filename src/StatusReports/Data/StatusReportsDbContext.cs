using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Models
{
    public class StatusReportsDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<ClientStatusReport> ClientStatusReports { get; set; }
        public DbSet<IndividualStatusReport> IndividualStatusReports { get; set; }
        public DbSet<IndividualStatusItem> IndividualStatusItems { get; set; }
        public DbSet<ClientStatusReportEmail> ClientStatusReportEmails { get; set; }

    }
}
