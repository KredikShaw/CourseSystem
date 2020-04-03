namespace CourseSystem.Web.ViewModels.Courses
{
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class CreatedCourseViewModel : IMapFrom<Course>
    {
        public string Name { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Difficulty { get; set; }

        public int EnrolledUsersCount { get; set; }
    }
}