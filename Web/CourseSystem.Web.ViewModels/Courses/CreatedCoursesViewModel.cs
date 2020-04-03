namespace CourseSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreatedCoursesViewModel
    {
        public IEnumerable<CreatedCourseViewModel> Courses { get; set; }
    }
}
