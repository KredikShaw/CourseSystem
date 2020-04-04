namespace CourseSystem.Web.ViewModels.Lessons
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LessonViewModel
    {
        public string LessonId { get; set; }

        public string CourseId { get; set; }

        public int PlaceInOrder { get; set; }
    }
}
