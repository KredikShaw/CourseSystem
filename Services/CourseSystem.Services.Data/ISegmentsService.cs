namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISegmentsService
    {
        Task CreateSegmentAsync(string content, string lessonId, int placeInLessonOrder);

        IEnumerable<T> GetSegments<T>(string lessonId);
    }
}
