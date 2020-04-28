namespace CourseSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CourseSystem.Data;
    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Data.Repositories;
    using CourseSystem.Services.Mapping;
    using CourseSystem.Web.ViewModels.Lessons;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class LessonsServiceTests
    {
        [Fact]
        public async Task CreateLessonTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));

            var mockUserLessonRepository = new Mock<IRepository<UserLesson>>();
            var mockCourseRepository = new Mock<IDeletableEntityRepository<Course>>();

            var service = new LessonsService(lessonsRepository, mockUserLessonRepository.Object, mockCourseRepository.Object);
            await service.CreateLessonAsync("Lesson1", "Descriptionnnnnnn", "1");
            await service.CreateLessonAsync("Lesson2", "Descriptionn234nnnnn", "1");
            await service.CreateLessonAsync("Lesson3", "Descript234onnnnnnn", "2");

            var lessons = lessonsRepository.All();
            Assert.Equal(3, lessons.Count());
        }

        [Fact]
        public async Task GetLessonsGenericTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var lesson in this.GetLessonData())
            {
                await lessonsRepository.AddAsync(lesson);
                await lessonsRepository.SaveChangesAsync();
            }

            var mockUserLessonRepository = new Mock<IRepository<UserLesson>>();
            var mockCourseRepository = new Mock<IDeletableEntityRepository<Course>>();
            AutoMapperConfig.RegisterMappings(typeof(StudyLessonViewModel).Assembly);
            var service = new LessonsService(lessonsRepository, mockUserLessonRepository.Object, mockCourseRepository.Object);
            var lessons = service.GetLessons<StudyLessonViewModel>("course1");
            Assert.Equal(2, lessons.Count());
        }

        [Fact]
        public async Task GetLessonsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var lesson in this.GetLessonData())
            {
                await lessonsRepository.AddAsync(lesson);
                await lessonsRepository.SaveChangesAsync();
            }

            var mockUserLessonRepository = new Mock<IRepository<UserLesson>>();
            var mockCourseRepository = new Mock<IDeletableEntityRepository<Course>>();
            var service = new LessonsService(lessonsRepository, mockUserLessonRepository.Object, mockCourseRepository.Object);
            var lessons = service.GetLessons("course1");
            Assert.Equal(2, lessons.Count());
        }

        [Fact]
        public async Task GetLesson()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var item in this.GetLessonData())
            {
                await lessonsRepository.AddAsync(item);
                await lessonsRepository.SaveChangesAsync();
            }

            var mockUserLessonRepository = new Mock<IRepository<UserLesson>>();
            var mockCourseRepository = new Mock<IDeletableEntityRepository<Course>>();
            AutoMapperConfig.RegisterMappings(typeof(EditLessonViewModel).Assembly);
            var service = new LessonsService(lessonsRepository, mockUserLessonRepository.Object, mockCourseRepository.Object);
            var lesson = service.GetLesson<EditLessonViewModel>("1");
            Assert.Equal("course1", lesson.CourseId);
            Assert.Equal("Description1", lesson.Description);
            Assert.Equal("Lesson 1", lesson.Name);
        }

        [Fact]
        public async Task GetLessonNameTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var item in this.GetLessonData())
            {
                await lessonsRepository.AddAsync(item);
                await lessonsRepository.SaveChangesAsync();
            }

            var mockUserLessonRepository = new Mock<IRepository<UserLesson>>();
            var mockCourseRepository = new Mock<IDeletableEntityRepository<Course>>();
            var service = new LessonsService(lessonsRepository, mockUserLessonRepository.Object, mockCourseRepository.Object);
            var lessonName = service.GetLessonName("1");
            var lessonName2 = service.GetLessonName("2");
            Assert.Equal("Lesson 1", lessonName);
            Assert.Equal("Lesson 2", lessonName2);
        }

        [Fact]
        public async Task EditLessonTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var item in this.GetLessonData())
            {
                await lessonsRepository.AddAsync(item);
                await lessonsRepository.SaveChangesAsync();
            }

            var mockUserLessonRepository = new Mock<IRepository<UserLesson>>();
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            foreach (var item in this.GetCourseData())
            {
                await courseRepository.AddAsync(item);
                await courseRepository.SaveChangesAsync();
            }

            var service = new LessonsService(lessonsRepository, mockUserLessonRepository.Object, courseRepository);
            await service.EditLesson("1", "New Name", "New Description", "User1");
            await service.EditLesson("3", "New Name2", "New Description2", "User2");

            var lesson1 = lessonsRepository.All().FirstOrDefault(x => x.Id == "1");
            var lesson3 = lessonsRepository.All().FirstOrDefault(x => x.Id == "3");

            Assert.Equal("New Name", lesson1.Name);
            Assert.Equal("New Description", lesson1.Description);
            Assert.Equal("Lesson 3", lesson3.Name);
            Assert.Equal("Description3", lesson3.Description);
        }

        [Fact]
        public async Task DeleteLessonTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var item in this.GetLessonData())
            {
                await lessonsRepository.AddAsync(item);
                await lessonsRepository.SaveChangesAsync();
            }

            var mockUserLessonRepository = new Mock<IRepository<UserLesson>>();
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            foreach (var item in this.GetCourseData())
            {
                await courseRepository.AddAsync(item);
                await courseRepository.SaveChangesAsync();
            }

            var service = new LessonsService(lessonsRepository, mockUserLessonRepository.Object, courseRepository);
            await service.DeleteLesson("1", "User1");
            await service.DeleteLesson("3", "User2");

            var lessons = lessonsRepository.All();
            Assert.Equal(3, lessons.Count());
        }

        [Fact]
        public async Task CreateUserLessonTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockLessonRepository = new Mock<IDeletableEntityRepository<Lesson>>();
            var mockCourseRepository = new Mock<IDeletableEntityRepository<Course>>();
            var userLessonRepository = new EfRepository<UserLesson>(new ApplicationDbContext(options.Options));
            var service = new LessonsService(mockLessonRepository.Object, userLessonRepository, mockCourseRepository.Object);
            await service.CreateUserLesson("User1", "1");
            await service.CreateUserLesson("User2", "2");
            var userLessons = userLessonRepository.All();
            Assert.Equal(2, userLessons.Count());
        }

        [Fact]
        public async Task GetCompletedLessonsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var lessonRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var lesson in this.GetLessonData())
            {
                await lessonRepository.AddAsync(lesson);
                await lessonRepository.SaveChangesAsync();
            }

            var mockCourseRepository = new Mock<IDeletableEntityRepository<Course>>();
            var userLessonRepository = new EfRepository<UserLesson>(new ApplicationDbContext(options.Options));
            var service = new LessonsService(lessonRepository, userLessonRepository, mockCourseRepository.Object);
            await service.CreateUserLesson("User1", "1");
            await service.CreateUserLesson("User1", "2");
            await service.CreateUserLesson("User2", "2");
            var lessonsPassed = service.GetCompletedLessons("course1", "User1");
            Assert.Equal(2, lessonsPassed);
        }

        private IQueryable<Lesson> GetLessonData()
        {
            return new List<Lesson>
            {
                new Lesson
                {
                    Id = "1",
                    CourseId = "course1",
                    Description = "Description1",
                    Name = "Lesson 1",
                },
                new Lesson
                {
                    Id = "2",
                    CourseId = "course1",
                    Description = "Description2",
                    Name = "Lesson 2",
                },
                new Lesson
                {
                    Id = "3",
                    CourseId = "course2",
                    Description = "Description3",
                    Name = "Lesson 3",
                },
                new Lesson
                {
                    Id = "4",
                    CourseId = "course3",
                    Description = "Description4",
                    Name = "Lesson 4",
                },
            }.AsQueryable();
        }

        private IQueryable<Course> GetCourseData()
        {
            return new List<Course>
            {
                new Course
                {
                    Id = "course1",
                    CategoryId = 1,
                    Description = "Description1",
                    Difficulty = "Expert",
                    Name = "Course 1",
                    ThumbnailUrl = "asdf",
                    UserId = "User1",
                },
                new Course
                {
                    Id = "course2",
                    CategoryId = 1,
                    Description = "Description1234",
                    Difficulty = "Advanced",
                    Name = "Course 2",
                    ThumbnailUrl = "asdf",
                    UserId = "User1",
                },
                new Course
                {
                    Id = "course3",
                    CategoryId = 1,
                    Description = "Desc1234ription1",
                    Difficulty = "Expert",
                    Name = "Course 3",
                    ThumbnailUrl = "asdf",
                    UserId = "User2",
                },
            }.AsQueryable();
        }
    }
}
