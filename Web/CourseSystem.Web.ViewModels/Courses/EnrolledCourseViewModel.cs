namespace CourseSystem.Web.ViewModels.Courses
{

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class EnrolledCourseViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Difficulty { get; set; }

        public int LessonsCount { get; set; }

        public int CompletedLessons { get; set; }
    }
}
