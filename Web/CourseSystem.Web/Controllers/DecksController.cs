namespace CourseSystem.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Decks;
    using Microsoft.AspNetCore.Mvc;

    public class DecksController : Controller
    {
        private readonly IDecksService decksService;

        public DecksController(IDecksService decksService)
        {
            this.decksService = decksService;
        }

        public IActionResult MyDecks()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var decks = this.decksService.GetDecks<DeckViewModel>(userId);
            var viewModel = new DisplayDecksViewModel();
            viewModel.Decks = decks;
            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string isPublic)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.decksService.CreateDeck(name, isPublic, userId);
            return this.RedirectToAction("MyDecks");
        }
    }
}
