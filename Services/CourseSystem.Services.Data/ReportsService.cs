namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class ReportsService : IReportsService
    {
        private readonly IDeletableEntityRepository<Report> reportsRepository;

        public ReportsService(IDeletableEntityRepository<Report> reportsRepository)
        {
            this.reportsRepository = reportsRepository;
        }

        public async Task CreateReportAsync(string title, string description, string courseId, string userId)
        {
            var report = new Report
            {
                Title = title,
                Description = description,
                CourseId = courseId,
                UserId = userId,
            };

            await this.reportsRepository.AddAsync(report);
            await this.reportsRepository.SaveChangesAsync();
        }

        public async Task DeleteReportAsync(string reportId)
        {
            var report = this.reportsRepository
                .All()
                .FirstOrDefault(x => x.Id == reportId);

            this.reportsRepository.Delete(report);
            await this.reportsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllReports<T>()
        {
            var reports = this.reportsRepository
                .All()
                .To<T>()
                .ToList();

            return reports;
        }
    }
}
