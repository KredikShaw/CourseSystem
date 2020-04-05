namespace CourseSystem.Web.ViewModels.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class StudySegmentViewModel : IMapFrom<Segment>
    {
        public int PlaceInLessonOrder { get; set; }

        public string Content { get; set; }
    }
}
