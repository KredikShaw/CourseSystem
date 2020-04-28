namespace CourseSystem.Web.ViewModels.Lessons
{
    using System.Collections.Generic;

    using CourseSystem.Data.Models;

    public class StudyLessonsViewModel
    {
        public IEnumerable<StudyLessonViewModel> Lessons { get; set; }

        public IEnumerable<UserLesson> UserLessons { get; set; }
    }
}
