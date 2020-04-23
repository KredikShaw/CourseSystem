namespace CourseSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CourseSystem.Data;
    using CourseSystem.Data.Models;
    using CourseSystem.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UsersLessonsServiceTests
    {
        [Fact]
        public async Task GetAllUserLessonsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var usersLessonsRepository = new EfRepository<UserLesson>(new ApplicationDbContext(options.Options));
            foreach (var userLesson in this.GetUsersLessonData())
            {
                await usersLessonsRepository.AddAsync(userLesson);
                await usersLessonsRepository.SaveChangesAsync();
            }

            var service = new UsersLessonsService(usersLessonsRepository);
            var userLessons = service.GetAllUserLessons();
            Assert.Equal(3, userLessons.Count());
        }

        private IQueryable<UserLesson> GetUsersLessonData()
        {
            return new List<UserLesson>
            {
                new UserLesson
                {
                    LessonId = "lesson1",
                    UserId = "User1",
                },
                new UserLesson
                {
                    LessonId = "lesson1",
                    UserId = "User2",
                },
                new UserLesson
                {
                    LessonId = "lesson2",
                    UserId = "User1",
                },
            }.AsQueryable();
        }
    }
}
