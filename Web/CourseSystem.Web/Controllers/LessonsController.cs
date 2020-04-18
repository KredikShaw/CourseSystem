namespace CourseSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Courses;
    using CourseSystem.Web.ViewModels.Lessons;
    using CourseSystem.Web.ViewModels.Segments;
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

        public IActionResult CreateLesson(string courseId)
        {
            var viewModel = new CourseIdViewModel
            {
                CourseId = courseId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(string title, string topic, int placeInOrder, string courseId)
        {
            var lesson = await this.lessonsService.CreateLessonAsync(title, topic, courseId);

            var viewModel = new LessonViewModel
            {
                LessonId = lesson.Id,
                PlaceInOrder = placeInOrder,
                CourseId = courseId,
            };

            return this.RedirectToAction("CreateSegment", "Segments", viewModel);
        }

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

        public IActionResult EditLessons(string courseId)
        {
            var viewModel = new StudyLessonsViewModel
            {
                Lessons = this.lessonsService.GetLessons<StudyLessonViewModel>(courseId),
            };
            return this.View(viewModel);
        }

        public IActionResult EditLesson(string lessonId)
        {
            var viewModel = this.lessonsService.GetLesson<EditLessonViewModel>(lessonId);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditLesson(string lessonId, string courseId, string name, string description)
        {
            await this.lessonsService.EditLesson(lessonId, name, description);
            return this.RedirectToAction("EditSegments", "Segments", new { lessonId });
        }

        public async Task<IActionResult> DeleteLesson(string lessonId, string courseId)
        {
            await this.lessonsService.DeleteLesson(lessonId);
            return this.RedirectToAction("EditLessons", "Lessons", new { courseId });
        }

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
