namespace CourseSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using CourseSystem.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class CourseInputModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        [MinLength(3, ErrorMessage = "Should be at least 3 symbols long")]
        public string Name { get; set; }

        public string Category { get; set; }

        public string Difficulty { get; set; }

        public IFormFile Thumbnail { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        [MinLength(20, ErrorMessage = "Should be at least 20 symbols long")]
        public string Description { get; set; }
    }
}
