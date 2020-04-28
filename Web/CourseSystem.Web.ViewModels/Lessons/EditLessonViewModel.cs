namespace CourseSystem.Web.ViewModels.Lessons
{
    using System.ComponentModel.DataAnnotations;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class EditLessonViewModel : IMapFrom<Lesson>
    {
        public string Id { get; set; }

        public string CourseId { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        [MinLength(3, ErrorMessage = "Should be at least 3 symbols long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        [MinLength(20, ErrorMessage = "Should be at least 20 symbols long")]
        public string Description { get; set; }
    }
}
