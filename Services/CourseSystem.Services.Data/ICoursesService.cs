namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;

    public interface ICoursesService
    {
        Task<Course> CreateCourseAsync(string name, string category, string difficulty, string imageUri, string description, string userId);

        IEnumerable<T> GetAllUserCourses<T>(string userId);

        IEnumerable<T> GetCoursesByCategory<T>(int categoryId, string userId);

        IEnumerable<T> GetCreatedCourses<T>(string userId);

        IEnumerable<T> GetEnrolledCourses<T>(string userId);

        Task EnrollStudentAsync(string courseId, string userId);

        string UploadImageToCloudinary(Stream imageFileStream);

        string UploadImageToCloudinaryBase64(string base64);

        T GetCourse<T>(string courseId);

        Task EditCourse(string courseId, string name, string category, string difficulty, string imageUri, string description);

        Task DeleteCourse(string courseId);

        IEnumerable<T> GetAllCourses<T>();

        Task UndeleteCourse(string courseId);
    }
}
