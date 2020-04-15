namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISegmentsService
    {
        Task CreateSegmentAsync(
            string content,
            string lessonId,
            string question,
            string correctAnswer,
            string wrongAnswer1,
            string wrongAnswer2,
            string wrongAnswer3,
            int placeInLessonOrder,
            string discriminator);

        IEnumerable<T> GetSegments<T>(string lessonId);

        Task UpdateContentSegment(string segmentId, string content);

        Task UpdateTestSegment(string segmentId, string question, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3);

        Task DeleteSegment(string segmentId);
    }
}
