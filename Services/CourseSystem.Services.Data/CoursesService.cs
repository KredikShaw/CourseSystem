namespace CourseSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        public CoursesService(IDeletableEntityRepository<Course> coursesRepository)
        {
            this.coursesRepository = coursesRepository;
        }

        public IEnumerable<T> GetAllCourses<T>()
        {
            var categories = this.coursesRepository
                .All()
                .To<T>()
                .ToList();

            return categories;
        }

        public IEnumerable<T> GetCoursesByCategory<T>(int categoryId)
        {
            var categories = this.coursesRepository
                .All()
                .Where(x => x.CategoryId == categoryId)
                .To<T>()
                .ToList();

            return categories;
        }
    }
}
