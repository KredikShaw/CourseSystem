namespace CourseSystem.Web.ViewModels.Lessons
{
    using System.Collections.Generic;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;
    using CourseSystem.Web.ViewModels.Segments;

    public class StudyLessonViewModel : IMapFrom<Lesson>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CourseId { get; set; }

        public IEnumerable<StudySegmentsViewModel> Segments { get; set; }
    }
}
