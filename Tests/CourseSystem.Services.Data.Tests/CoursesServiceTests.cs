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
    using CourseSystem.Web.ViewModels.Administration.Dashboard;
    using CourseSystem.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Xunit;

    public class CoursesServiceTests
    {
        [Fact]
        public async Task GetAllCoursesTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));

            var coursesToAdd = this.GetCourseData();
            foreach (var course in coursesToAdd)
            {
                await courseRepository.AddAsync(course);
                await courseRepository.SaveChangesAsync();
            }

            var mockUserCourseRepository = new Mock<IRepository<UserCourse>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, mockUserCourseRepository.Object, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            AutoMapperConfig.RegisterMappings(typeof(AdminCourseViewModel).Assembly);

            var courses = service.GetAllCourses<AdminCourseViewModel>();
            Assert.Equal(4, courses.Count());
        }

        [Fact]
        public async Task GetAllUserCoursesTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userCourseRepository = new EfRepository<UserCourse>(new ApplicationDbContext(options.Options));

            var coursesToAdd = this.GetCourseData();
            var userCoursesToAdd = this.GetUserCourseData();
            foreach (var course in coursesToAdd)
            {
                await courseRepository.AddAsync(course);
                await courseRepository.SaveChangesAsync();
            }

            foreach (var userCourse in userCoursesToAdd)
            {
                await userCourseRepository.AddAsync(userCourse);
                await userCourseRepository.SaveChangesAsync();
            }

            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, userCourseRepository, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            AutoMapperConfig.RegisterMappings(typeof(DiscoverCourseViewModel).Assembly);

            var courses = service.GetAllUserCourses<DiscoverCourseViewModel>("User1");
            Assert.Equal(3, courses.Count());
        }

        [Fact]
        public async Task CreateCourseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));

            var mockUserCourseRepository = new Mock<IRepository<UserCourse>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, mockUserCourseRepository.Object, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);

            await service.CreateCourseAsync("Course1", "Art", "Beginner", "totallyvalidurl", "Course Descriptiooooon", "User1");
            var courses = courseRepository.All();

            Assert.Equal(1, courses.Count());
            Assert.Equal("Course1", courses.FirstOrDefault().Name);
        }

        [Fact]
        public async Task GetCoursesByCategoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));

            var mockUserCourseRepository = new Mock<IRepository<UserCourse>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, mockUserCourseRepository.Object, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            AutoMapperConfig.RegisterMappings(typeof(DiscoverCourseViewModel).Assembly);
            var coursesToAdd = this.GetCourseData();
            foreach (var course in coursesToAdd)
            {
                await courseRepository.AddAsync(course);
                await courseRepository.SaveChangesAsync();
            }

            var courses = service.GetCoursesByCategory<DiscoverCourseViewModel>(1, "1");

            Assert.Equal(2, courses.Count());
        }

        [Fact]
        public async Task GetCreatedCourses()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));

            var mockUserCourseRepository = new Mock<IRepository<UserCourse>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, mockUserCourseRepository.Object, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            AutoMapperConfig.RegisterMappings(typeof(CreatedCourseViewModel).Assembly);
            var coursesToAdd = this.GetCourseData();
            foreach (var course in coursesToAdd)
            {
                await courseRepository.AddAsync(course);
                await courseRepository.SaveChangesAsync();
            }

            var courses = service.GetCreatedCourses<CreatedCourseViewModel>("User1");

            Assert.Equal(3, courses.Count());
        }

        [Fact]
        public async Task GetEnrolledCourses()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userCourseRepository = new EfRepository<UserCourse>(new ApplicationDbContext(options.Options));

            var coursesToAdd = this.GetCourseData();
            var userCoursesToAdd = this.GetUserCourseData();
            foreach (var course in coursesToAdd)
            {
                await courseRepository.AddAsync(course);
                await courseRepository.SaveChangesAsync();
            }

            foreach (var userCourse in userCoursesToAdd)
            {
                await userCourseRepository.AddAsync(userCourse);
                await userCourseRepository.SaveChangesAsync();
            }

            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, userCourseRepository, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            AutoMapperConfig.RegisterMappings(typeof(EnrolledCourseViewModel).Assembly);

            var courses = service.GetEnrolledCourses<EnrolledCourseViewModel>("User1");
            Assert.Single(courses);
        }

        [Fact]
        public async Task EnrollStudentTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userCourseRepository = new EfRepository<UserCourse>(new ApplicationDbContext(options.Options));

            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, userCourseRepository, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            await service.EnrollStudentAsync("1", "User2");
            var userCourses = userCourseRepository.All();

            Assert.Single(userCourses);
            Assert.Contains("User2", userCourses.Select(x => x.UserId));
            Assert.Contains("1", userCourses.Select(x => x.CourseId));
        }

        [Fact]
        public async Task GetCourseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));
            var userCourseRepository = new EfRepository<UserCourse>(new ApplicationDbContext(options.Options));

            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, userCourseRepository, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            AutoMapperConfig.RegisterMappings(typeof(EnrolledCourseViewModel).Assembly);
            var coursesToAdd = this.GetCourseData();
            foreach (var item in coursesToAdd)
            {
                await courseRepository.AddAsync(item);
                await courseRepository.SaveChangesAsync();
            }

            var course = service.GetCourse<EnrolledCourseViewModel>("1");
            Assert.Equal("Course1", course.Name);
        }

        [Fact]
        public async Task EditCourseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));

            var mockUserCourseRepository = new Mock<IRepository<UserCourse>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, mockUserCourseRepository.Object, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            var coursesToAdd = this.GetCourseData();
            foreach (var item in coursesToAdd)
            {
                await courseRepository.AddAsync(item);
                await courseRepository.SaveChangesAsync();
            }

            await service.EditCourse("1", "EditedName", "1", "Expert", "stillnothing", "NewDescription", "User1");
            var course = courseRepository.All().FirstOrDefault(x => x.Id == "1");

            Assert.Equal("EditedName", course.Name);
            Assert.Equal("Expert", course.Difficulty);
            Assert.Equal("stillnothing", course.ThumbnailUrl);
            Assert.Equal("User1", course.UserId);
        }

        [Fact]
        public async Task DeleteCourseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));

            var mockUserCourseRepository = new Mock<IRepository<UserCourse>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, mockUserCourseRepository.Object, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            var coursesToAdd = this.GetCourseData();
            foreach (var item in coursesToAdd)
            {
                await courseRepository.AddAsync(item);
                await courseRepository.SaveChangesAsync();
            }

            await service.DeleteCourse("1", "User1");
            await service.DeleteCourse("2", "User2");

            var courses = courseRepository.All();

            Assert.Equal(3, courses.Count());
        }

        [Fact]
        public async Task UndeleteCourseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var courseRepository = new EfDeletableEntityRepository<Course>(new ApplicationDbContext(options.Options));

            var mockUserCourseRepository = new Mock<IRepository<UserCourse>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockCategoryService = new Mock<ICategoriesService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var service = new CoursesService(courseRepository, mockUserCourseRepository.Object, mockConfiguration.Object, mockCategoryService.Object, mockUserManager.Object);
            var coursesToAdd = this.GetCourseData();
            foreach (var item in coursesToAdd)
            {
                await courseRepository.AddAsync(item);
                await courseRepository.SaveChangesAsync();
            }

            var course = new Course
            {
                Name = "Course5",
                Id = "6",
                UserId = "User3",
                Description = "Course Description 134",
                CategoryId = 4,
                IsDeleted = true,
            };
            await courseRepository.AddAsync(course);
            await courseRepository.SaveChangesAsync();

            await service.UndeleteCourse("6");

            var courses = courseRepository.AllWithDeleted();

            Assert.Equal(5, courses.Count());
        }

        private IQueryable<Course> GetCourseData()
        {
            return new List<Course>
            {
                new Course
                {
                    Name = "Course1",
                    Id = "1",
                    UserId = "User1",
                    Description = "Course Description 1",
                    CategoryId = 1,
                },
                new Course
                {
                    Name = "Course2",
                    Id = "2",
                    UserId = "User1",
                    Description = "Course Description 2",
                    CategoryId = 1,
                },
                new Course
                {
                    Name = "Course3",
                    Id = "3",
                    UserId = "User2",
                    Description = "Course Description 3",
                    CategoryId = 3,
                },
                new Course
                {
                    Name = "Course4",
                    Id = "4",
                    UserId = "User1",
                    Description = "Course Description 4",
                    CategoryId = 5,
                },
            }.AsQueryable();
        }

        private IQueryable<UserCourse> GetUserCourseData()
        {
            return new List<UserCourse>
            {
                new UserCourse
                {
                    UserId = "User1",
                    CourseId = "3",
                },
            }.AsQueryable();
        }
    }
}
