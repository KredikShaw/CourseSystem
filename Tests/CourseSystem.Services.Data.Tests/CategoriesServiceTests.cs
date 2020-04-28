namespace CourseSystem.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;
    using CourseSystem.Web.ViewModels.Categories;
    using Moq;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public void GetCategoriesTest()
        {
            var mockRepository = new Mock<IDeletableEntityRepository<Category>>();
            mockRepository.Setup(x => x.All()).Returns(this.GetCategoryData());
            var service = new CategoriesService(mockRepository.Object);
            AutoMapperConfig.RegisterMappings(typeof(CategoryViewModel).Assembly);
            var categories = service.GetCategories<CategoryViewModel>().OrderBy(x => x.Name);
            var firstCategory = categories.First();

            Assert.Equal("Art", firstCategory.Name);
        }

        [Fact]
        public void GetCategoryIdTest()
        {
            var mockRepository = new Mock<IDeletableEntityRepository<Category>>();
            mockRepository.Setup(x => x.All()).Returns(this.GetCategoryData());
            var service = new CategoriesService(mockRepository.Object);

            var categoryId = service.GetCategoryId("Art");
            Assert.Equal(1, categoryId);
        }

        private IQueryable<Category> GetCategoryData()
        {
            return new List<Category>
            {
                new Category
                {
                    Name = "Art",
                    Id = 1,
                },
                new Category
                {
                    Name = "Sports",
                    Id = 2,
                },
                new Category
                {
                    Name = "Beauty",
                    Id = 3,
                },
                new Category
                {
                    Name = "Gardening",
                    Id = 4,
                },
            }.AsQueryable();
        }
    }
}
