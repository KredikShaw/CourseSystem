namespace CourseSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EnrolledCoursesViewModel
    {
        public IEnumerable<EnrolledCourseViewModel> Courses { get; set; }
    }
}
