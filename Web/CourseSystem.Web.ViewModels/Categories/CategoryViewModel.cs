namespace CourseSystem.Web.ViewModels.Categories
{
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
