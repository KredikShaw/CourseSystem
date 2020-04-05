namespace CourseSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Courses;
    using CourseSystem.Web.ViewModels.Lessons;
    using CourseSystem.Web.ViewModels.Segments;
    using Microsoft.AspNetCore.Mvc;

    public class LessonsController : Controller
    {
        private readonly ILessonsService lessonsService;
        private readonly ISegmentsService segmentsService;

        public LessonsController(ILessonsService lessonsService, ISegmentsService segmentsService)
        {
            this.lessonsService = lessonsService;
            this.segmentsService = segmentsService;
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
                CourseId = courseId,
            };

            return this.View(viewModel);
        }
    }
}
