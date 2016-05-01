using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace StatusReports.Models
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<StatusReportsDbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

            var random = new Random();

            if (!context.Clients.Any())
            {
                // Person
                context.People.AddRange(
                    new Person {  FirstName = "Edgar", LastName="Nzokwe", Active=true, UserName="nzokwe@rdacorp.com",  PhotoUrl = "/images/edgar.jpg" },
                    new Person { FirstName = "Golois", LastName = "Mouelet", Active = true, UserName = "mouelet@rdacorp.com", PhotoUrl = "/images/golois.jpg" }
                );

                // Client
                context.Clients.AddRange(
                    new Client { Name = "CareCam", ImageUrl = "/images/carecam.jpg" },
                    new Client { Name = "Becton-Dickison", ImageUrl = "/images/bd.png" }
                );
                context.SaveChanges();

                // Project
                context.Projects.AddRange(
                    new Project { Name = "CareCam - Phase 1", Active = true, ClientId = 1 },
                    new Project { Name = "CareCam - Support", Active = true, ClientId = 1 },
                    new Project { Name = "BD - Infostratus", Active = true, ClientId = 2 }
                );

                // Week
                context.Weeks.AddRange(
                    new Week { EndingDate = new DateTime(2016, 4, 1) },
                    new Week { EndingDate = new DateTime(2016, 4, 8) },
                    new Week { EndingDate = new DateTime(2016, 4, 15) },
                    new Week { EndingDate = new DateTime(2016, 4, 22) },
                    new Week { EndingDate = new DateTime(2016, 4, 29) }
                );
                context.SaveChanges();

                // Individual Status Reports
                context.IndividualStatusReports.AddRange(
                    new IndividualStatusReport { PersonId = 1, WeekId = 1, Status = StatusCode.Approved, ProjectId = 1 },
                    new IndividualStatusReport { PersonId = 1, WeekId = 2, Status = StatusCode.Approved, ProjectId = 1 },
                    new IndividualStatusReport { PersonId = 1, WeekId = 3, Status = StatusCode.Submitted, ProjectId = 1 },
                    new IndividualStatusReport { PersonId = 1, WeekId = 4, Status = StatusCode.Draft, ProjectId = 1 },
                    new IndividualStatusReport { PersonId = 1, WeekId = 5, Status = StatusCode.Draft, ProjectId = 1 },
                    new IndividualStatusReport { PersonId = 1, WeekId = 1, Status = StatusCode.Approved, ProjectId = 2 },
                    new IndividualStatusReport { PersonId = 1, WeekId = 2, Status = StatusCode.Approved, ProjectId = 2 },
                    new IndividualStatusReport { PersonId = 1, WeekId = 3, Status = StatusCode.Submitted, ProjectId = 2 },
                    new IndividualStatusReport { PersonId = 1, WeekId = 4, Status = StatusCode.Draft, ProjectId = 2 },
                    new IndividualStatusReport { PersonId = 1, WeekId = 5, Status = StatusCode.Draft, ProjectId = 2 }
                );

                context.SaveChanges();

                // Topic Questions
                foreach (IndividualStatusReport r in context.IndividualStatusReports)
                {

                    for (int i = 1; i < 7; i++)
                    {

                        context.IndividualStatusItems.Add(
                        new IndividualStatusItem()
                        {
                            WorkDescription = lorem.Substring(random.Next(0, 100), random.Next(0, 50)),
                            Date = r.Week.EndingDate.AddDays(-i),
                            Hours = 8,
                            IndividualStatusReportId = r.Id
                        });
                    }

                }
                context.SaveChanges();

            }
        }
    }
}
