namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReportsService
    {
        Task CreateReportAsync(string title, string description, string courseId, string userId);

        IEnumerable<T> GetAllReports<T>();

        Task DeleteReportAsync(string reportId);
    }
}
