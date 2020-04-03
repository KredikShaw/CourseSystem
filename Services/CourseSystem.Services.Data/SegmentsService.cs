namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;

    public class SegmentsService : ISegmentsService
    {
        private readonly IDeletableEntityRepository<Segment> segmentsRepository;

        public SegmentsService(IDeletableEntityRepository<Segment> segmentsRepository)
        {
            this.segmentsRepository = segmentsRepository;
        }

        public async Task CreateSegmentAsync(string content, string lessonId, int placeInLessonOrder)
        {
            var segment = new Segment
            {
                Content = content,
                LessonId = lessonId,
                PlaceInLessonOrder = placeInLessonOrder,
                Discriminator = "ContentSegment",
            };

            await this.segmentsRepository.AddAsync(segment);
            await this.segmentsRepository.SaveChangesAsync();
        }
    }
}
