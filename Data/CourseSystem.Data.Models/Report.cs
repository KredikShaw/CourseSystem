namespace CourseSystem.Data.Models
{
    using System;

    using CourseSystem.Data.Common.Models;

    public class Report : BaseDeletableModel<string>
    {
        public Report()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
