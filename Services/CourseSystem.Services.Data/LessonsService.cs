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

    public class LessonsService : ILessonsService
    {
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;

        public LessonsService(IDeletableEntityRepository<Lesson> lessonsRepository)
        {
            this.lessonsRepository = lessonsRepository;
        }

        public async Task<Lesson> CreateLessonAsync(string title, string description, string courseId)
        {
            var lesson = new Lesson
            {
                Name = title,
                Description = description,
                CourseId = courseId,
            };

            await this.lessonsRepository.AddAsync(lesson);
            await this.lessonsRepository.SaveChangesAsync();

            return lesson;
        }

        public string GetLessonName(string lessonId)
        {
            string name = this.lessonsRepository
                .All()
                .Where(x => x.Id == lessonId)
                .Select(x => x.Name)
                .FirstOrDefault();

            return name;
        }

        public IEnumerable<T> GetLessons<T>(string courseId)
        {
            var lessons = this.lessonsRepository.All()
                .Where(x => x.CourseId == courseId)
                .To<T>()
                .ToList();

            return lessons;
        }
    }
}
