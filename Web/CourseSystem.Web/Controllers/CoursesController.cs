namespace CourseSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
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
    }
}
