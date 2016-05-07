using StatusReports.Controllers;
using StatusReports.Data;
using StatusReports.Models;
using StatusReports.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StatusReports.Test
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class StatusReportControllerTest : IClassFixture<StatusReportsDbFixture>
    {
        private StatusReportsDbFixture statusReportDbFixture;
        private IRepository<IndividualStatusReport> statusReportRepo;
        private StatusReportController statusReportController;
        public StatusReportControllerTest(StatusReportsDbFixture fixture)
        {
            statusReportDbFixture = fixture;
            statusReportDbFixture.InitializeDatabase();
            statusReportRepo = new StatusReportRepository(statusReportDbFixture.Context);
            statusReportController = new StatusReportController(statusReportRepo);
        }

        [Fact]
        public async Task CreateTest_ValidStatusReport()
        {
            //Arrange
            var statusReportUser = statusReportDbFixture.Context.People.FirstOrDefault();
            var project = statusReportDbFixture.Context.Projects.FirstOrDefault();
            var week = statusReportDbFixture.Context.Weeks.FirstOrDefault();
            var statusItems = new List<IndividualStatusItem>
            {
                new IndividualStatusItem { Hours=8 },
                new IndividualStatusItem { Hours=7 },
                new IndividualStatusItem { Hours=10 },
                new IndividualStatusItem { Hours=8 },
                new IndividualStatusItem { Hours=9 },
            };
            var statusReportVM = new StatusReportViewModel()
            {
                LookupData = await statusReportRepo.GetLookupDataAsync(),
                StatusReport = new IndividualStatusReport
                {
                    PersonId = statusReportUser.PersonId,
                    Person = statusReportUser,
                    Project = project,
                    ProjectId = project.Id,
                    Week = week,
                    WeekId = week.Id,
                    IndividualStatusItems = statusItems
                }
            };
            var oldStatusReportCount = statusReportDbFixture.Context.IndividualStatusReports.Count();
            var oldStatusItemCount = statusReportDbFixture.Context.IndividualStatusItems.Count();

            //Act
            await statusReportController.Create(statusReportVM, "", "");

            //Assert
            var newStatusReportCount = statusReportDbFixture.Context.IndividualStatusReports.Count();
            var newStatusItemCount = statusReportDbFixture.Context.IndividualStatusItems.Count();
            Assert.True((oldStatusReportCount + 1) == newStatusReportCount);
            Assert.True((oldStatusItemCount + statusItems.Count) == newStatusItemCount);

        }
    }
}
