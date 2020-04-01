namespace CourseSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class LessonsController : Controller
    {
        private readonly ILessonsService lessonsService;

        public LessonsController(ILessonsService lessonsService)
        {
            this.lessonsService = lessonsService;
        }

        public IActionResult CreateLesson(string courseId)
        {
            return this.View(new CourseIdViewModel { CourseId = courseId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(string title, string topic, string courseId)
        {
            var lesson = await this.lessonsService.CreateLessonAsync(title, topic, courseId);
            return this.View(new CourseIdViewModel { CourseId = courseId });
        }
    }
}
