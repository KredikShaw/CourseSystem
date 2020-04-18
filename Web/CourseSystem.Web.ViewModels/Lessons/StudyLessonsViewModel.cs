namespace CourseSystem.Web.ViewModels.Lessons
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Data.Models;

    public class StudyLessonsViewModel
    {
        public IEnumerable<StudyLessonViewModel> Lessons { get; set; }

        public IEnumerable<UserLesson> UserLessons { get; set; }
    }
}
