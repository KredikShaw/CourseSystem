namespace CourseSystem.Data.Models
{
    using System;

    using CourseSystem.Data.Common.Models;

    public class Segment : BaseDeletableModel<string>
    {
        public Segment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int PlaceInLessonOrder { get; set; }

        public string LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }

        public string Content { get; set; }

        public string Question { get; set; }

        public string CorrectAnswer { get; set; }

        public string Discriminator { get; set; }
    }
}
