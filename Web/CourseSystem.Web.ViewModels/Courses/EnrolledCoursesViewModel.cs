namespace CourseSystem.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    public class EnrolledCoursesViewModel
    {
        public IEnumerable<EnrolledCourseViewModel> Courses { get; set; }
    }
}
