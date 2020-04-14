namespace CourseSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserCourse
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
