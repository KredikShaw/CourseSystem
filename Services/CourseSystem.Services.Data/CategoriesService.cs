namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetCategories<T>()
        {
            var categories = this.categoriesRepository
                .All()
                .To<T>()
                .ToList();

            return categories;
        }
    }
}
