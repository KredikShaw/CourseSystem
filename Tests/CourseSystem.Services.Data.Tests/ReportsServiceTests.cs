namespace CourseSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CourseSystem.Data;
    using CourseSystem.Data.Models;
    using CourseSystem.Data.Repositories;
    using CourseSystem.Services.Mapping;
    using CourseSystem.Web.ViewModels.Administration.Dashboard;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ReportsServiceTests
    {
        [Fact]
        public async Task CreateReportTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var reportsRepository = new EfDeletableEntityRepository<Report>(new ApplicationDbContext(options.Options));
            var service = new ReportsService(reportsRepository);

            await service.CreateReportAsync("Title1", "Description1", "course1", "User1");
            await service.CreateReportAsync("Title2", "Description2", "course2", "User1");

            var reports = reportsRepository.All();

            Assert.Equal(2, reports.Count());
        }

        [Fact]
        public async Task GetAllReportsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var reportsRepository = new EfDeletableEntityRepository<Report>(new ApplicationDbContext(options.Options));
            foreach (var report in this.GetReportsData())
            {
                await reportsRepository.AddAsync(report);
                await reportsRepository.SaveChangesAsync();
            }

            AutoMapperConfig.RegisterMappings(typeof(ReportViewModel).Assembly);
            var service = new ReportsService(reportsRepository);

            var reports = service.GetAllReports<ReportViewModel>();
            Assert.Equal(4, reports.Count());
        }

        [Fact]
        public async Task DeleteReportTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var reportsRepository = new EfDeletableEntityRepository<Report>(new ApplicationDbContext(options.Options));
            foreach (var report in this.GetReportsData())
            {
                await reportsRepository.AddAsync(report);
                await reportsRepository.SaveChangesAsync();
            }

            var service = new ReportsService(reportsRepository);

            Assert.Equal(4, reportsRepository.All().Count());
            await service.DeleteReportAsync("1");
            Assert.Equal(3, reportsRepository.All().Count());
        }

        private IQueryable<Report> GetReportsData()
        {
            return new List<Report>
            {
                new Report
                {
                    Id = "1",
                    CourseId = "course1",
                    Description = "Description1",
                    Title = "Title1",
                    UserId = "User1",
                },
                new Report
                {
                    Id = "2",
                    CourseId = "course2",
                    Description = "Description2",
                    Title = "Title2",
                    UserId = "User1",
                },
                new Report
                {
                    Id = "3",
                    CourseId = "course2",
                    Description = "Description234",
                    Title = "Titsdfle1",
                    UserId = "User2",
                },
                new Report
                {
                    Id = "4",
                    CourseId = "course3",
                    Description = "Descript234ion1",
                    Title = "Titsdfsdle1",
                    UserId = "User2",
                },
            }.AsQueryable();
        }
    }
}
