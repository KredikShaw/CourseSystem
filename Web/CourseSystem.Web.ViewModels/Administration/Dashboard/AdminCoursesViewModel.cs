namespace CourseSystem.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    public class AdminCoursesViewModel
    {
        public IEnumerable<AdminCourseViewModel> Courses { get; set; }
    }
}
