namespace CourseSystem.Web.Areas.Administration.Controllers
{
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IReportsService reportsService;

        public DashboardController(ISettingsService settingsService, IReportsService reportsService)
        {
            this.settingsService = settingsService;
            this.reportsService = reportsService;
        }

        public IActionResult Index()
        {
            var viewModel = new ReportsViewModel
            {
                Reports = this.reportsService.GetAllReports<ReportViewModel>(),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> CompleteReport(string reportId)
        {
            await this.reportsService.DeleteReportAsync(reportId);
            return this.Redirect("/");
        }
    }
}
