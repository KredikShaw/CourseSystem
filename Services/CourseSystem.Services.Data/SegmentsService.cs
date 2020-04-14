namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class SegmentsService : ISegmentsService
    {
        private readonly IDeletableEntityRepository<Segment> segmentsRepository;

        public SegmentsService(IDeletableEntityRepository<Segment> segmentsRepository)
        {
            this.segmentsRepository = segmentsRepository;
        }

        public async Task CreateSegmentAsync(
            string content,
            string lessonId,
            string question,
            string correctAnswer,
            string wrongAnswer1,
            string wrongAnswer2,
            string wrongAnswer3,
            int placeInLessonOrder,
            string discriminator)
        {
            var segment = new Segment
            {
                Content = content,
                LessonId = lessonId,
                PlaceInLessonOrder = placeInLessonOrder,
                Question = question,
                CorrectAnswer = correctAnswer,
                WrongAnswer1 = wrongAnswer1,
                WrongAnswer2 = wrongAnswer2,
                WrongAnswer3 = wrongAnswer3,
                Discriminator = discriminator,
            };

            await this.segmentsRepository.AddAsync(segment);
            await this.segmentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetSegments<T>(string lessonId)
        {
            var segments = this.segmentsRepository.All()
                   .Where(x => x.LessonId == lessonId)
                   .To<T>()
                   .ToList();

            return segments;
        }

        public async Task UpdateContentSegment(string segmentId, string content)
        {
            var segment = this.segmentsRepository
                .All()
                .FirstOrDefault(x => x.Id == segmentId);

            segment.Content = content;

            this.segmentsRepository.Update(segment);
            await this.segmentsRepository.SaveChangesAsync();
        }

        public async Task UpdateTestSegment(string segmentId, string question, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
        {
            var segment = this.segmentsRepository
                .All()
                .FirstOrDefault(x => x.Id == segmentId);

            segment.Question = question;
            segment.CorrectAnswer = correctAnswer;
            segment.WrongAnswer1 = wrongAnswer1;
            segment.WrongAnswer2 = wrongAnswer2;
            segment.WrongAnswer3 = wrongAnswer3;

            this.segmentsRepository.Update(segment);
            await this.segmentsRepository.SaveChangesAsync();
        }
    }
}
