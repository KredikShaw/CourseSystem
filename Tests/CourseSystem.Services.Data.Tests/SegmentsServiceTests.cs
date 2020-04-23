namespace CourseSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CourseSystem.Data;
    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Data.Repositories;
    using CourseSystem.Services.Mapping;
    using CourseSystem.Web.ViewModels.Segments;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class SegmentsServiceTests
    {
        [Fact]
        public async Task CreateSegmentTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var segmentsRepository = new EfDeletableEntityRepository<Segment>(new ApplicationDbContext(options.Options));
            var mockLessonsRepository = new Mock<IDeletableEntityRepository<Lesson>>();
            var mockCoursesRepository = new Mock<IDeletableEntityRepository<Course>>();
            var service = new SegmentsService(segmentsRepository, mockLessonsRepository.Object, mockCoursesRepository.Object);
            await service.CreateSegmentAsync("Content1", "lesson1", "Question1", "Correct1", "FirstWrong1", "SecondWrong1", "ThirdWrong1", 4, "Mixed");
            await service.CreateSegmentAsync("Content1", "lesson2", null, null, null, null, null, 1, "ContentSegment");
            await service.CreateSegmentAsync(null, "lesson1", "Question1", "Correct1", "FirstWrong1", "SecondWrong1", "ThirdWrong1", 3, "TestSegment");
            Assert.Equal(3, segmentsRepository.All().Count());
        }

        [Fact]
        public async Task GetSegmentsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var segmentsRepository = new EfDeletableEntityRepository<Segment>(new ApplicationDbContext(options.Options));
            foreach (var segment in this.GetSegmentsData())
            {
                await segmentsRepository.AddAsync(segment);
                await segmentsRepository.SaveChangesAsync();
            }

            var mockLessonsRepository = new Mock<IDeletableEntityRepository<Lesson>>();
            var mockCoursesRepository = new Mock<IDeletableEntityRepository<Course>>();
            AutoMapperConfig.RegisterMappings(typeof(StudySegmentViewModel).Assembly);
            var service = new SegmentsService(segmentsRepository, mockLessonsRepository.Object, mockCoursesRepository.Object);
            var segments = service.GetSegments<StudySegmentViewModel>("lesson1");
            Assert.Equal(2, segments.Count());
        }

        [Fact]
        public async Task UpdateContentSegmentTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var segmentsRepository = new EfDeletableEntityRepository<Segment>(new ApplicationDbContext(options.Options));
            foreach (var segment in this.GetSegmentsData())
            {
                await segmentsRepository.AddAsync(segment);
                await segmentsRepository.SaveChangesAsync();
            }

            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var lesson in this.GetLessonData())
            {
                await lessonsRepository.AddAsync(lesson);
                await lessonsRepository.SaveChangesAsync();
            }

            var coursesRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            foreach (var course in this.GetCourseData())
            {
                await coursesRepository.AddAsync(course);
                await coursesRepository.SaveChangesAsync();
            }

            var service = new SegmentsService(segmentsRepository, lessonsRepository, coursesRepository);
            await service.UpdateContentSegment("1", "NewContent", "User1");
            await service.UpdateContentSegment("3", "NewerContent", "User2");
            var segment1 = segmentsRepository.All().FirstOrDefault(x => x.Id == "1");
            var segment3 = segmentsRepository.All().FirstOrDefault(x => x.Id == "3");
            Assert.Equal("NewContent", segment1.Content);
            Assert.Equal("ASDsfdfsdOFKASDFPOKddsfdfdASDFPOAdsfSDF<h1>asdf</h1>", segment3.Content);
        }

        [Fact]
        public async Task UpdateTestSegmentTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var segmentsRepository = new EfDeletableEntityRepository<Segment>(new ApplicationDbContext(options.Options));
            foreach (var item in this.GetSegmentsData())
            {
                await segmentsRepository.AddAsync(item);
                await segmentsRepository.SaveChangesAsync();
            }

            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var lesson in this.GetLessonData())
            {
                await lessonsRepository.AddAsync(lesson);
                await lessonsRepository.SaveChangesAsync();
            }

            var coursesRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            foreach (var course in this.GetCourseData())
            {
                await coursesRepository.AddAsync(course);
                await coursesRepository.SaveChangesAsync();
            }

            var service = new SegmentsService(segmentsRepository, lessonsRepository, coursesRepository);
            await service.UpdateTestSegment("2", "NewQuestion", "NewCorrect", "NewWrong1", "NewWrong2", "NewWrong3", "User1");
            var segment = segmentsRepository.All().FirstOrDefault(x => x.Id == "2");
            Assert.Equal("NewQuestion", segment.Question);
            Assert.Equal("NewWrong2", segment.WrongAnswer2);
            await service.UpdateTestSegment("2", "NewQqweqweestion", "qweNewCoqweqwerrect", "Ne2343ewwWrong1", "NewWron213dfg2", "NewW234sdfrong3", "User2");
            var segment2 = segmentsRepository.All().FirstOrDefault(x => x.Id == "2");
            Assert.Equal("NewQuestion", segment2.Question);
            Assert.Equal("NewWrong2", segment2.WrongAnswer2);
        }

        [Fact]
        public async Task DeleteSegmentTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var segmentsRepository = new EfDeletableEntityRepository<Segment>(new ApplicationDbContext(options.Options));
            foreach (var item in this.GetSegmentsData())
            {
                await segmentsRepository.AddAsync(item);
                await segmentsRepository.SaveChangesAsync();
            }

            var lessonsRepository = new EfDeletableEntityRepository<Lesson>(new ApplicationDbContext(options.Options));
            foreach (var lesson in this.GetLessonData())
            {
                await lessonsRepository.AddAsync(lesson);
                await lessonsRepository.SaveChangesAsync();
            }

            var coursesRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            foreach (var course in this.GetCourseData())
            {
                await coursesRepository.AddAsync(course);
                await coursesRepository.SaveChangesAsync();
            }

            var service = new SegmentsService(segmentsRepository, lessonsRepository, coursesRepository);
            await service.DeleteSegment("1", "User1");
            Assert.Equal(2, segmentsRepository.All().Count());
            await service.DeleteSegment("2", "User2");
            Assert.Equal(2, segmentsRepository.All().Count());
        }

        private IQueryable<Segment> GetSegmentsData()
        {
            return new List<Segment>
            {
                new Segment
                {
                    Id = "1",
                    Content = "ASDOFKASDFPOKASDFPOASDF<h1>asdf</h1>",
                    CorrectAnswer = null,
                    Discriminator = "ContentSegment",
                    LessonId = "lesson1",
                    PlaceInLessonOrder = 1,
                    Question = null,
                    WrongAnswer1 = null,
                    WrongAnswer2 = null,
                    WrongAnswer3 = null,
                },
                new Segment
                {
                    Id = "2",
                    Content = null,
                    CorrectAnswer = "Correct",
                    Discriminator = "TestSegment",
                    LessonId = "lesson1",
                    PlaceInLessonOrder = 2,
                    Question = "Question",
                    WrongAnswer1 = "Wrong1",
                    WrongAnswer2 = "Wrong2",
                    WrongAnswer3 = "Wrong3",
                },
                new Segment
                {
                    Id = "3",
                    Content = "ASDsfdfsdOFKASDFPOKddsfdfdASDFPOAdsfSDF<h1>asdf</h1>",
                    CorrectAnswer = null,
                    Discriminator = "ContentSegment",
                    LessonId = "lesson2",
                    PlaceInLessonOrder = 5,
                    Question = null,
                    WrongAnswer1 = null,
                    WrongAnswer2 = null,
                    WrongAnswer3 = null,
                },
            }.AsQueryable();
        }

        private IQueryable<Lesson> GetLessonData()
        {
            return new List<Lesson>
            {
                new Lesson
                {
                    Id = "lesson1",
                    CourseId = "course1",
                    Description = "Description1",
                    Name = "Lesson 1",
                },
                new Lesson
                {
                    Id = "lesson2",
                    CourseId = "course1",
                    Description = "Description2",
                    Name = "Lesson 2",
                },
                new Lesson
                {
                    Id = "lesson3",
                    CourseId = "course2",
                    Description = "Description3",
                    Name = "Lesson 3",
                },
                new Lesson
                {
                    Id = "lesson4",
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
