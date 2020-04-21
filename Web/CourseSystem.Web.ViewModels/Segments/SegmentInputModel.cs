namespace CourseSystem.Web.ViewModels.Segments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class SegmentInputModel
    {
        public int PlaceInLessonOrder { get; set; }

        public string LessonId { get; set; }

        public string CourseId { get; set; }

        [MinLength(3, ErrorMessage = "Minimum 3 symbols")]
        public string Content { get; set; }

        [MinLength(3, ErrorMessage = "Minimum 3 symbols")]
        public string Question { get; set; }

        [MinLength(3, ErrorMessage = "Minimum 3 symbols")]
        public string CorrectAnswer { get; set; }

        [MinLength(3, ErrorMessage = "Minimum 3 symbols")]
        public string WrongAnswer1 { get; set; }

        [MinLength(3, ErrorMessage = "Minimum 3 symbols")]
        public string WrongAnswer2 { get; set; }

        [MinLength(3, ErrorMessage = "Minimum 3 symbols")]
        public string WrongAnswer3 { get; set; }

        public string Discriminator { get; set; }
    }
}
