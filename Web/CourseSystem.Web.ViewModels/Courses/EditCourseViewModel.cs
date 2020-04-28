namespace CourseSystem.Web.ViewModels.Courses
{
    using System.ComponentModel.DataAnnotations;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditCourseViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        [MinLength(3, ErrorMessage = "Should be at least 3 symbols long")]
        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Difficulty { get; set; }

        public string ThumbnailUrl { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        [MinLength(20, ErrorMessage = "Should be at least 20 symbols long")]
        public string Description { get; set; }

        public IFormFile Thumbnail { get; set; }
    }
}
