namespace CourseSystem.Web.Controllers
{

    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Discover()
        {
            var viewModel = new CategoriesViewModel
            {
                Categories = this.categoriesService.GetCategories<CategoryViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
