namespace CourseSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(ICoursesService coursesService, UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult EnrolledCourses()
        {
            return this.View();
        }

        public IActionResult CreatedCourses()
        {
            return this.View();
        }

        public IActionResult CreateCourse()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(string name, string category, string difficulty, IFormFile thumbnail, string description)
        {
            var imageUri = this.coursesService.UploadImageToCloudinary(thumbnail.OpenReadStream());
            var userId = this.userManager.GetUserId(this.User);

            await this.coursesService.CreateCourseAsync(name, category, difficulty, imageUri, description, userId);

            return this.View();
        }

        public IActionResult DiscoverAll()
        {
            var viewModel = new DiscoverCoursesViewModel
            {
                Courses = this.coursesService.GetAllCourses<DiscoverCourseViewModel>(),
            };
            return this.View("Discover", viewModel);
        }

        public IActionResult DiscoverByCategory(int categoryId)
        {
            var viewModel = new DiscoverCoursesViewModel
            {
                Courses = this.coursesService.GetCoursesByCategory<DiscoverCourseViewModel>(categoryId),
            };
            return this.View("Discover", viewModel);
        }
    }
}
