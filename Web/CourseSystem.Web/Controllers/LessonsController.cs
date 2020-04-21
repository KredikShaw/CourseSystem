namespace CourseSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Courses;
    using CourseSystem.Web.ViewModels.Lessons;
    using CourseSystem.Web.ViewModels.Segments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class LessonsController : Controller
    {
        private readonly ILessonsService lessonsService;
        private readonly ISegmentsService segmentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public LessonsController(ILessonsService lessonsService, ISegmentsService segmentsService, UserManager<ApplicationUser> userManager)
        {
            this.lessonsService = lessonsService;
            this.segmentsService = segmentsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult CreateLesson(string courseId)
        {
            var viewModel = new LessonInputModel
            {
                CourseId = courseId,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateLesson(LessonInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var lesson = await this.lessonsService.CreateLessonAsync(inputModel.Name, inputModel.Description, inputModel.CourseId);

            var viewModel = new SegmentInputModel
            {
                LessonId = lesson.Id,
                PlaceInLessonOrder = inputModel.PlaceInOrder,
                CourseId = inputModel.CourseId,
            };

            return this.RedirectToAction("CreateSegment", "Segments", viewModel);
        }

        [Authorize]
        public IActionResult Study(string lessonId, string courseId)
        {
            var viewModel = new StudySegmentsViewModel
            {
                Name = this.lessonsService.GetLessonName(lessonId),
                Segments = this.segmentsService.GetSegments<StudySegmentViewModel>(lessonId),
                LessonId = lessonId,
                CourseId = courseId,
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult EditLessons(string courseId)
        {
            var viewModel = new StudyLessonsViewModel
            {
                Lessons = this.lessonsService.GetLessons<StudyLessonViewModel>(courseId),
            };
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult EditLesson(string lessonId)
        {
            var viewModel = this.lessonsService.GetLesson<EditLessonViewModel>(lessonId);
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditLesson(EditLessonViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.lessonsService.EditLesson(inputModel.Id, inputModel.Name, inputModel.Description, userId);
            return this.RedirectToAction("EditSegments", "Segments", new { lessonId = inputModel.Id, courseId = inputModel.CourseId });
        }

        [Authorize]
        public async Task<IActionResult> DeleteLesson(string lessonId, string courseId)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.lessonsService.DeleteLesson(lessonId, userId);
            return this.RedirectToAction("EditLessons", "Lessons", new { courseId });
        }

        [Authorize]
        public async Task<IActionResult> CompleteLesson(string lessonId, string courseId)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.lessonsService.CreateUserLesson(userId, lessonId);

            var viewModel = new CourseIdViewModel
            {
                CourseId = courseId,
            };

            return this.RedirectToAction("Study", "Courses", viewModel);
        }
    }
}
