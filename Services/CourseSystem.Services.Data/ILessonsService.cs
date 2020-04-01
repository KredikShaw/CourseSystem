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
    }
}
