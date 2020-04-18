namespace CourseSystem.Web.ViewModels.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;
    using Ganss.XSS;

    public class StudySegmentViewModel : IMapFrom<Segment>
    {
        public string Id { get; set; }

        public string LessonId { get; set; }

        public int PlaceInLessonOrder { get; set; }

        public string Content { get; set; }

        public string Question { get; set; }//TODO: Get the test in the view in a view component

        public string CorrectAnswer { get; set; }

        public string WrongAnswer1 { get; set; }

        public string WrongAnswer2 { get; set; }

        public string WrongAnswer3 { get; set; }

        public string Discriminator { get; set; }

        public string SanitizedContent
        {
            get
            {
                var html = new HtmlSanitizer();
                html.AllowedTags.Add("iframe");
                return html.Sanitize(this.Content);
            }
        }
    }
}
