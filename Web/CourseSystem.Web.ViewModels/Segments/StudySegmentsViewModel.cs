namespace CourseSystem.Web.ViewModels.Segments
{
    using System.Collections.Generic;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class StudySegmentsViewModel : IMapFrom<Segment>
    {
        public string LessonId { get; set; }

        public string CourseId { get; set; }

        public string Name { get; set; }

        public IEnumerable<StudySegmentViewModel> Segments { get; set; }
    }
}
