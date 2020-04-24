namespace CourseSystem.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IReportsService reportsService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(ISettingsService settingsService, IReportsService reportsService, ICoursesService coursesService, UserManager<ApplicationUser> userManager)
        {
            this.settingsService = settingsService;
            this.reportsService = reportsService;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var viewModel = new ReportsViewModel
            {
                Reports = this.reportsService.GetAllReports<ReportViewModel>(),
            };
            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CompleteReport(string reportId)
        {
            await this.reportsService.DeleteReportAsync(reportId);
            return this.Redirect("/");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Courses()
        {
            var viewModel = new AdminCoursesViewModel
            {
                Courses = this.coursesService.GetAllCourses<AdminCourseViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCourse(string courseId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.coursesService.DeleteCourse(courseId, userId);
            return this.RedirectToAction("Courses");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UndeleteCourse(string courseId)
        {
            await this.coursesService.UndeleteCourse(courseId);
            return this.RedirectToAction("Courses");
        }
    }
}
