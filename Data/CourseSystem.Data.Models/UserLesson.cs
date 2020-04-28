namespace CourseSystem.Data.Models
{
    using System.Collections.Generic;

    public class UserLesson
    {
        public static IEnumerable<object> Count { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
