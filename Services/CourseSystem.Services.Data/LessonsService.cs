namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class LessonsService : ILessonsService
    {
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;
        private readonly IRepository<UserLesson> usersLessonsRepository;
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        public LessonsService(IDeletableEntityRepository<Lesson> lessonsRepository, IRepository<UserLesson> usersLessonsRepository, IDeletableEntityRepository<Course> coursesRepository)
        {
            this.lessonsRepository = lessonsRepository;
            this.usersLessonsRepository = usersLessonsRepository;
            this.coursesRepository = coursesRepository;
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

        public async Task CreateUserLesson(string userId, string lessonId)
        {
            if (!this.usersLessonsRepository.All().Any(x => x.LessonId == lessonId && x.UserId == userId))
            {
                var userLesson = new UserLesson
                {
                    UserId = userId,
                    LessonId = lessonId,
                };
                await this.usersLessonsRepository.AddAsync(userLesson);
                await this.usersLessonsRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteLesson(string lessonId, string userId)
        {
            var lesson = this.lessonsRepository
                .All()
                .FirstOrDefault(x => x.Id == lessonId);
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == lesson.CourseId);
            if (course.UserId == userId)
            {
                this.lessonsRepository.Delete(lesson);
                await this.lessonsRepository.SaveChangesAsync();
            }
        }

        public async Task EditLesson(string lessonId, string name, string description, string userId)
        {
            var lesson = this.lessonsRepository
                .All()
                .FirstOrDefault(l => l.Id == lessonId);
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == lesson.CourseId);
            if (course.UserId == userId)
            {
                lesson.Name = name;
                lesson.Description = description;

                this.lessonsRepository.Update(lesson);
                await this.lessonsRepository.SaveChangesAsync();
            }
        }

        public int GetCompletedLessons(string courseId, string userId)
        {
            var lessonIds = this.GetLessons(courseId).Select(x => x.Id);
            var userLessons = this.usersLessonsRepository.All().ToList();
            var count = 0;
            foreach (var userLesson in userLessons)
            {
                if (lessonIds.Contains(userLesson.LessonId) && userLesson.UserId == userId)
                {
                    count++;
                }
            }

            return count;
        }

        public T GetLesson<T>(string lessonId)
        {
            var lesson = this.lessonsRepository
                .All()
                .Where(l => l.Id == lessonId)
                .To<T>()
                .FirstOrDefault();

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

        public IEnumerable<Lesson> GetLessons(string courseId)
        {
            var lessons = this.lessonsRepository.All()
                .Where(x => x.CourseId == courseId)
                .ToList();

            return lessons;
        }
    }
}
