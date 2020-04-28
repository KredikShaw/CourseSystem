namespace CourseSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Categories;
    using CourseSystem.Web.ViewModels.Courses;
    using CourseSystem.Web.ViewModels.Lessons;
    using CourseSystem.Web.ViewModels.Reports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly ICategoriesService categoriesService;
        private readonly ILessonsService lessonsService;
        private readonly IReportsService reportsService;
        private readonly IUsersLessonsService usersLessonsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(
            ICoursesService coursesService,
            ICategoriesService categoriesService,
            ILessonsService lessonsService,
            IReportsService reportsService,
            IUsersLessonsService usersLessonsService,
            UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.categoriesService = categoriesService;
            this.lessonsService = lessonsService;
            this.reportsService = reportsService;
            this.usersLessonsService = usersLessonsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult EnrolledCourses()
        {
            var viewModel = new EnrolledCoursesViewModel
            {
                Courses = this.coursesService.GetEnrolledCourses<EnrolledCourseViewModel>(this.userManager.GetUserId(this.User)),
            };
            foreach (var course in viewModel.Courses)
            {
                course.CompletedLessons = this.lessonsService.GetCompletedLessons(course.Id, this.userManager.GetUserId(this.User));
            }

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult CreatedCourses()
        {
            var viewModel = new CreatedCoursesViewModel
            {
                Courses = this.coursesService.GetCreatedCourses<CreatedCourseViewModel>(this.userManager.GetUserId(this.User)),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult CreateCourse()
        {
            var viewModel = new CourseInputModel
            {
                Categories = this.categoriesService.GetCategories<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var imageUri = string.Empty;
            if (inputModel.Thumbnail != null)
            {
                imageUri = this.coursesService.UploadImageToCloudinary(inputModel.Thumbnail.OpenReadStream());
            }

            var userId = this.userManager.GetUserId(this.User);

            var course = await this.coursesService.CreateCourseAsync(inputModel.Name, inputModel.Category, inputModel.Difficulty, imageUri, inputModel.Description, userId);

            return this.RedirectToAction(actionName: "CreateLesson", controllerName: "Lessons", routeValues: new CourseIdViewModel { CourseId = course.Id });
        }

        [Authorize]
        public IActionResult DiscoverAll()
        {
            var viewModel = new DiscoverCoursesViewModel
            {
                Courses = this.coursesService.GetAllUserCourses<DiscoverCourseViewModel>(this.userManager.GetUserId(this.User)),
            };
            return this.View("Discover", viewModel); // TODO: More info button on discover courses, send to another page for the course
        }

        [Authorize]
        public IActionResult DiscoverByCategory(int categoryId)
        {
            var viewModel = new DiscoverCoursesViewModel
            {
                Courses = this.coursesService.GetCoursesByCategory<DiscoverCourseViewModel>(categoryId, this.userManager.GetUserId(this.User)),
            };
            return this.View("Discover", viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Enroll(string courseId)
        {
            await this.coursesService.EnrollStudentAsync(courseId, this.userManager.GetUserId(this.User));
            return this.RedirectToAction("EnrolledCourses");
        }

        [Authorize]
        public IActionResult Study(string courseId)
        {
            var viewModel = new StudyLessonsViewModel
            {
                Lessons = this.lessonsService.GetLessons<StudyLessonViewModel>(courseId),
                UserLessons = this.usersLessonsService.GetAllUserLessons(),
            };
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult EditCourse(string courseId)
        {
            var viewModel = new EditCourseWithCategoryViewModel<EditCourseViewModel>()
            {
                Course = this.coursesService.GetCourse<EditCourseViewModel>(courseId),
                Categories = this.categoriesService.GetCategories<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditCourse(EditCourseViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var imageUri = string.Empty;
            if (inputModel.Thumbnail != null)
            {
                imageUri = this.coursesService.UploadImageToCloudinary(inputModel.Thumbnail.OpenReadStream());
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.coursesService.EditCourse(inputModel.Id, inputModel.Name, inputModel.CategoryName, inputModel.Difficulty, imageUri, inputModel.Description, userId);

            var viewModel = new CourseIdViewModel
            {
                CourseId = inputModel.Id,
            };
            return this.RedirectToAction("EditLessons", "Lessons", viewModel);
        }

        [Authorize]
        public async Task<IActionResult> DeleteCourse(string courseId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.coursesService.DeleteCourse(courseId, userId);
            return this.Redirect("/Courses/CreatedCourses");
        }

        [Authorize]
        public IActionResult Report(string courseId)
        {
            var viewModel = new ReportInputModel
            {
                CourseId = courseId,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Report(ReportInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.reportsService.CreateReportAsync(inputModel.Title, inputModel.Description, inputModel.CourseId, userId);
            return this.RedirectToAction("EnrolledCourses");
        }
    }
}
