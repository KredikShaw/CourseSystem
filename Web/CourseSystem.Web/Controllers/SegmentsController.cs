namespace CourseSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;

    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Lessons;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class SegmentsController : Controller
    {
        private readonly ISegmentsService segmentsService;
        private readonly ICoursesService coursesService;

        public SegmentsController(ISegmentsService segmentsService, ICoursesService coursesService)
        {
            this.segmentsService = segmentsService;
            this.coursesService = coursesService;
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
        public async Task<IActionResult> CreateSegment(
            string content,
            string lessonId,
            string correctAnswer,
            string question,
            string wrongAnswer1,
            string wrongAnswer2,
            string wrongAnswer3,
            string courseId,
            int placeInOrder,
            string discriminator,
            string submitType)
        {
            if (discriminator == "ContentSegment")
            {
                var sources = Regex.Matches(content, "data:[^ ]* \\/>");
                foreach (var source in sources)
                {
                    var fix = source.ToString();
                    fix = fix.TrimStart('\\').TrimEnd('>').TrimEnd('/').TrimEnd(' ').TrimEnd('\"').Trim();

                    var result = this.UploadImage(fix);
                    content = content.Replace(fix, result);
                }
            }

            await this.segmentsService.CreateSegmentAsync(content, lessonId, question, correctAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3, placeInOrder, discriminator);

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

        public string UploadImage(string base64)
        {
            var url = this.coursesService.UploadImageToCloudinaryBase64(base64);
            return url;
        }
    }
}
