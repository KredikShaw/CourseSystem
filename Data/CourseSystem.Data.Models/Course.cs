namespace CourseSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Data.Common.Models;

    public class Course : BaseDeletableModel<string>
    {
        public Course()
        {
            this.Id = Guid.NewGuid().ToString();
            this.EnrolledUsers = new HashSet<UserCourse>();
            this.Lessons = new HashSet<Lesson>();
        }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Difficulty { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<UserCourse> EnrolledUsers { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
