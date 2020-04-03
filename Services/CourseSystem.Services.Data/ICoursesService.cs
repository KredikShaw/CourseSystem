namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;

    public interface ICoursesService
    {
        Task<Course> CreateCourseAsync(string name, string category, string difficulty, string imageUri, string description, string userId);

        IEnumerable<T> GetAllCourses<T>();

        IEnumerable<T> GetCoursesByCategory<T>(int categoryId);

        IEnumerable<T> GetCreatedCourses<T>(string userId);

        IEnumerable<T> GetEnrolledCourses<T>(string userId);

        string UploadImageToCloudinary(Stream imageFileStream);
    }
}
