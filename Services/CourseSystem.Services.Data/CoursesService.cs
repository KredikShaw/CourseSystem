namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;
    using Microsoft.Extensions.Configuration;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IRepository<UserCourse> usersCoursesRepository;
        private readonly IConfiguration configuration;
        private readonly ICategoriesService categoriesService;

        public CoursesService(
            IDeletableEntityRepository<Course> coursesRepository,
            IRepository<UserCourse> usersCoursesRepository,
            IConfiguration configuration,
            ICategoriesService categoriesService)
        {
            this.coursesRepository = coursesRepository;
            this.usersCoursesRepository = usersCoursesRepository;
            this.configuration = configuration;
            this.categoriesService = categoriesService;
        }

        public async Task<Course> CreateCourseAsync(string name, string category, string difficulty, string imageUri, string description, string userId)
        {
            var course = new Course
            {
                Name = name,
                CategoryId = this.categoriesService.GetCategoryId(category),
                Difficulty = difficulty,
                ThumbnailUrl = imageUri,
                Description = description,
                UserId = userId,
            };

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();

            return course;
        }

        public async Task EnrollStudentAsync(string courseId, string userId)
        {
            var userCourse = new UserCourse
            {
                CourseId = courseId,
                UserId = userId,
            };

            await this.usersCoursesRepository.AddAsync(userCourse);
            await this.usersCoursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllCourses<T>(string userId)
        {
            var userCourses = this.usersCoursesRepository
                .All()
                .Where(x => x.UserId == userId)
                .Select(x => x.CourseId)
                .ToList();

            var categories = this.coursesRepository
                .All()
                .Where(x => !userCourses.Contains(x.Id))
                .To<T>()
                .ToList();

            return categories;
        }

        public IEnumerable<T> GetCoursesByCategory<T>(int categoryId, string userId)
        {
            var categories = this.coursesRepository
                .All()
                .Where(x => x.CategoryId == categoryId)
                .To<T>()
                .ToList();

            return categories;
        }

        public IEnumerable<T> GetCreatedCourses<T>(string userId)
        {
            var courses = this.coursesRepository
                .All()
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToList();

            return courses;
        }

        public IEnumerable<T> GetEnrolledCourses<T>(string userId)
        {
            var courseIds = this.usersCoursesRepository
                .All()
                .Where(x => x.UserId == userId)
                .Select(x => x.CourseId)
                .ToList();

            List<T> courses = new List<T>();

            foreach (var courseId in courseIds)
            {
                var course = this.coursesRepository
                    .All()
                    .Where(x => x.Id == courseId)
                    .To<T>()
                    .FirstOrDefault();

                courses.Add(course);
            }

            return courses;
        }

        public string UploadImageToCloudinary(Stream imageFileStream)
        {
            Account account = new Account
            {
                Cloud = this.configuration.GetSection("Cloudinary").GetSection("cloudName").Value,
                ApiKey = this.configuration.GetSection("Cloudinary").GetSection("apiKey").Value,
                ApiSecret = this.configuration.GetSection("Cloudinary").GetSection("apiSecret").Value,
            };

            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription("thumbnail", imageFileStream),
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.SecureUri.AbsoluteUri;
        }
    }
}
