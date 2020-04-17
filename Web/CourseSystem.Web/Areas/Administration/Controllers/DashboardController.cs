namespace CourseSystem.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IReportsService reportsService;
        private readonly ICoursesService coursesService;

        public DashboardController(ISettingsService settingsService, IReportsService reportsService, ICoursesService coursesService)
        {
            this.settingsService = settingsService;
            this.reportsService = reportsService;
            this.coursesService = coursesService;
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

        public IActionResult Courses()
        {
            var viewModel = new AdminCoursesViewModel
            {
                Courses = this.coursesService.GetAllCourses<AdminCourseViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteCourse(string courseId)
        {
            await this.coursesService.DeleteCourse(courseId);
            return this.RedirectToAction("Courses");
        }

        public async Task<IActionResult> UndeleteCourse(string courseId)
        {
            await this.coursesService.UndeleteCourse(courseId);
            return this.RedirectToAction("Courses");
        }
    }
}
