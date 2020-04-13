namespace CourseSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class EditCourseViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Difficulty { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Description { get; set; }
    }
}
