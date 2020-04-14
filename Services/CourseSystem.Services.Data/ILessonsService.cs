namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;

    public interface ILessonsService
    {
        Task<Lesson> CreateLessonAsync(string title, string description, string courseId);

        IEnumerable<T> GetLessons<T>(string courseId);

        T GetLesson<T>(string lessonId);

        string GetLessonName(string lessonId);

        Task EditLesson(string lessonId, string name, string description);

        Task DeleteLesson(string lessonId);
    }
}
