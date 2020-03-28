namespace CourseSystem.Web.ViewModels.Courses
{
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class DiscoverCourseViewModel : IMapFrom<Course>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Difficulty { get; set; }

        public string ThumbnailUrl { get; set; }

        public int EnrolledUsersCount { get; set; }

        public int LessonsCount { get; set; }

        public string UserUserName { get; set; }
    }
}