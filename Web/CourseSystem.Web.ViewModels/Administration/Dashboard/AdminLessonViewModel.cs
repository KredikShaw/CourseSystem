namespace CourseSystem.Web.ViewModels.Administration.Dashboard
{
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class AdminLessonViewModel : IMapFrom<Lesson>
    {
        public string Name { get; set; }
    }
}
