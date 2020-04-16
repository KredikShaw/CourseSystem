namespace CourseSystem.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class ReportViewModel : IMapFrom<Report>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CourseId { get; set; }

        public string CourseName { get; set; }

        public string UserUserName { get; set; }

        public string CreatedOn { get; set; }
    }
}
