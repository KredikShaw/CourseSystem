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

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Lessons;
    using CourseSystem.Web.ViewModels.Segments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SegmentsController : Controller
    {
        private readonly ISegmentsService segmentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICoursesService coursesService;

        public SegmentsController(ISegmentsService segmentsService, ICoursesService coursesService, UserManager<ApplicationUser> userManager)
        {
            this.segmentsService = segmentsService;
            this.userManager = userManager;
            this.coursesService = coursesService;
        }

        [Authorize]
        public IActionResult CreateSegment(SegmentInputModel inputModel)
        {
            var viewModel = new SegmentInputModel
            {
                LessonId = inputModel.LessonId,
                PlaceInLessonOrder = inputModel.PlaceInLessonOrder + 1,
                CourseId = inputModel.CourseId,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSegment(SegmentInputModel inputModel, string submitType)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            if (inputModel.Discriminator == "ContentSegment")
            {
                var sources = Regex.Matches(inputModel.Content, "data:[^ ]* \\/>");
                foreach (var source in sources)
                {
                    var fix = source.ToString();
                    fix = fix.TrimStart('\\').TrimEnd('>').TrimEnd('/').TrimEnd(' ').TrimEnd('\"').Trim();

                    var result = this.UploadImage(fix);
                    inputModel.Content = inputModel.Content.Replace(fix, result);
                }
            }

            await this.segmentsService.CreateSegmentAsync(inputModel.Content, inputModel.LessonId, inputModel.Question, inputModel.CorrectAnswer, inputModel.WrongAnswer1, inputModel.WrongAnswer2, inputModel.WrongAnswer3, inputModel.PlaceInLessonOrder, inputModel.Discriminator);

            if (submitType == "another")
            {
                var viewModel = new SegmentInputModel
                {
                    LessonId = inputModel.LessonId,
                    PlaceInLessonOrder = inputModel.PlaceInLessonOrder,
                    CourseId = inputModel.CourseId,
                };

                return this.RedirectToAction("CreateSegment", viewModel);
            }
            else if (submitType == "finishLesson")
            {
                return this.RedirectToAction("CreateLesson", "Lessons", new { inputModel.CourseId });
            }
            else
            {
                return this.RedirectToAction("CreatedCourses", "Courses");
            }
        }

        [Authorize]
        public string UploadImage(string base64)
        {
            var url = this.coursesService.UploadImageToCloudinaryBase64(base64);
            return url;
        }

        [Authorize]
        public IActionResult EditSegments(string lessonId, string courseId)
        {
            var viewModel = new StudySegmentsViewModel
            {
                Segments = this.segmentsService.GetSegments<StudySegmentViewModel>(lessonId),
                LessonId = lessonId,
                CourseId = courseId,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditContentSegment(string segmentId, string lessonId, string content, string submitType)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.segmentsService.UpdateContentSegment(segmentId, content, userId);

            if (submitType == "save")
            {
                return this.RedirectToAction("EditSegments", new { lessonId });
            }
            else
            {
                return this.Redirect("/Courses/CreatedCourses");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditTestSegment(string segmentId, string lessonId, string question, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3, string submitType)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.segmentsService.UpdateTestSegment(segmentId, question, correctAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3, userId);

            if (submitType == "save")
            {
                return this.RedirectToAction("EditSegments", new { lessonId });
            }
            else
            {
                return this.Redirect("/Courses/CreatedCourses");
            }
        }

        [Authorize]
        public async Task<IActionResult> DeleteSegment(string segmentId, string lessonId)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.segmentsService.DeleteSegment(segmentId, userId);
            return this.RedirectToAction("EditSegments", "Segments", new { lessonId });
        }
    }
}
