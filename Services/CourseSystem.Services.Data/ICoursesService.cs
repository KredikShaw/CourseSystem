namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;

    public interface ICoursesService
    {
        IEnumerable<T> GetAllCourses<T>();

        IEnumerable<T> GetCoursesByCategory<T>(int categoryId);
    }
}
