namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Data.Models;

    public interface IUsersLessonsService
    {
        IEnumerable<UserLesson> GetAllUserLessons();
    }
}
