namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;

    public class UsersLessonsService : IUsersLessonsService
    {
        private readonly IRepository<UserLesson> userLessonRepository;

        public UsersLessonsService(IRepository<UserLesson> userLessonRepository)
        {
            this.userLessonRepository = userLessonRepository;
        }

        public IEnumerable<UserLesson> GetAllUserLessons()
        {
            var userLessons = this.userLessonRepository.All().ToList();
            return userLessons;
        }
    }
}
