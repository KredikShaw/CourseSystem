namespace CourseSystem.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AdminCoursesViewModel
    {
        public IEnumerable<AdminCourseViewModel> Courses { get; set; }
    }
}
