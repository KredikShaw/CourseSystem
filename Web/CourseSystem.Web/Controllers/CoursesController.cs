namespace CourseSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
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
