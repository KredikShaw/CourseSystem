namespace CourseSystem.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class AdminCourseViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserUserName { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<AdminLessonViewModel> Lessons { get; set; }
    }
}
