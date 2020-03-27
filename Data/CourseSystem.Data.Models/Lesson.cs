namespace CourseSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CourseSystem.Data.Common.Models;

    public class Lesson : BaseDeletableModel<string>
    {
        public Lesson()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Segments = new HashSet<Segment>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Segment> Segments { get; set; }
    }
}
