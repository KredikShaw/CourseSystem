namespace CourseSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Lessons;
    using Microsoft.AspNetCore.Mvc;

    public class SegmentsController : Controller
    {
        private readonly ISegmentsService segmentsService;

        public SegmentsController(ISegmentsService segmentsService)
        {
            this.segmentsService = segmentsService;
        }

        public IActionResult CreateSegment(string lessonId, string courseId, int placeInOrder)
        {
            var viewModel = new LessonViewModel
            {
                LessonId = lessonId,
                PlaceInOrder = placeInOrder + 1,
                CourseId = courseId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSegment(string content, string lessonId, string courseId, int placeInOrder, string submitType)
        {
            await this.segmentsService.CreateSegmentAsync(content, lessonId, placeInOrder);

            if (submitType == "another")
            {
                var viewModel = new LessonViewModel
                {
                    LessonId = lessonId,
                    PlaceInOrder = placeInOrder,
                    CourseId = courseId,
                };

                return this.RedirectToAction("CreateSegment", viewModel);
            }
            else if (submitType == "finishLesson")
            {
                return this.RedirectToAction("CreateLesson", "Lessons", new { courseId });
            }
            else
            {
                return this.RedirectToAction("CreatedCourses", "Courses");
            }
        }
    }
}
