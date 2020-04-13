namespace CourseSystem.Web.ViewModels.Lessons
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class EditLessonViewModel : IMapFrom<Lesson>
    {
        public string Id { get; set; }

        public string CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
