namespace CourseSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Categories;
    using CourseSystem.Web.ViewModels.Courses;
    using CourseSystem.Web.ViewModels.Lessons;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly ICategoriesService categoriesService;
        private readonly ILessonsService lessonsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(
            ICoursesService coursesService,
            ICategoriesService categoriesService,
            ILessonsService lessonsService,
            UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.categoriesService = categoriesService;
            this.lessonsService = lessonsService;
            this.userManager = userManager;
        }

        public IActionResult EnrolledCourses()
        {
            var viewModel = new EnrolledCoursesViewModel
            {
                Courses = this.coursesService.GetEnrolledCourses<EnrolledCourseViewModel>(this.userManager.GetUserId(this.User)),
            };
            return this.View(viewModel);
        }

        public IActionResult CreatedCourses()
        {
            var viewModel = new CreatedCoursesViewModel
            {
                Courses = this.coursesService.GetCreatedCourses<CreatedCourseViewModel>(this.userManager.GetUserId(this.User)),
            };

            return this.View(viewModel);
        }

        public IActionResult CreateCourse()
        {
            var viewModel = new CategoriesViewModel
            {
                Categories = this.categoriesService.GetCategories<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(string name, string category, string difficulty, IFormFile thumbnail, string description)
        {
            var imageUri = string.Empty;
            if (thumbnail != null)
            {
                imageUri = this.coursesService.UploadImageToCloudinary(thumbnail.OpenReadStream());
            }

            var userId = this.userManager.GetUserId(this.User);

            var course = await this.coursesService.CreateCourseAsync(name, category, difficulty, imageUri, description, userId);

            return this.RedirectToAction(actionName: "CreateLesson", controllerName: "Lessons", routeValues: new CourseIdViewModel { CourseId = course.Id });
        }

        public IActionResult DiscoverAll()
        {
            var viewModel = new DiscoverCoursesViewModel
            {
                Courses = this.coursesService.GetAllCourses<DiscoverCourseViewModel>(this.userManager.GetUserId(this.User)),
            };
            return this.View("Discover", viewModel);
        }

        public IActionResult DiscoverByCategory(int categoryId)
        {
            var viewModel = new DiscoverCoursesViewModel
            {
                Courses = this.coursesService.GetCoursesByCategory<DiscoverCourseViewModel>(categoryId, this.userManager.GetUserId(this.User)),
            };
            return this.View("Discover", viewModel);
        }

        public async Task<IActionResult> Enroll(string courseId)
        {
            await this.coursesService.EnrollStudentAsync(courseId, this.userManager.GetUserId(this.User));
            return this.RedirectToAction("EnrolledCourses");
        }

        public IActionResult Study(string courseId)
        {
            var viewModel = new StudyLessonsViewModel
            {
                Lessons = this.lessonsService.GetLessons<StudyLessonViewModel>(courseId),
            };
            return this.View(viewModel);
        }

        public IActionResult EditCourse(string courseId)
        {
            var viewModel = new EditCourseWithCategoryViewModel<EditCourseViewModel>()
            {
                Course = this.coursesService.GetCourse<EditCourseViewModel>(courseId),
                Categories = this.categoriesService.GetCategories<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(string courseId, string name, string category, string difficulty, IFormFile thumbnail, string description)
        {
            var imageUri = string.Empty;
            if (thumbnail != null)
            {
                imageUri = this.coursesService.UploadImageToCloudinary(thumbnail.OpenReadStream());
            }

            await this.coursesService.EditCourse(courseId, name, category, difficulty, imageUri, description);

            var viewModel = new CourseIdViewModel
            {
                CourseId = courseId,
            };
            return this.RedirectToAction("EditLessons", "Lessons", viewModel);
        }

        public async Task<IActionResult> DeleteCourse(string courseId)
        {
            await this.coursesService.DeleteCourse(courseId);
            return this.Redirect("/Courses/CreatedCourses");
        }
    }
}
