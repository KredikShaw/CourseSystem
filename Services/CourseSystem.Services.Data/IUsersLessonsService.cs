namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;

    using CourseSystem.Data.Models;

    public interface IUsersLessonsService
    {
        IEnumerable<UserLesson> GetAllUserLessons();
    }
}
