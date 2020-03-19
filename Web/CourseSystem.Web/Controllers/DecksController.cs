namespace CourseSystem.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Decks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DecksController : Controller
    {
        private readonly IDecksService decksService;
        private readonly UserManager<ApplicationUser> userManager;

        public DecksController(IDecksService decksService, UserManager<ApplicationUser> userManager)
        {
            this.decksService = decksService;
            this.userManager = userManager;
        }

        public IActionResult MyDecks()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var decks = this.decksService.GetDecks<DeckViewModel>(userId);
            var viewModel = new DisplayDecksViewModel
            {
                Decks = decks,
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string isPublic)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var deck = await this.decksService.CreateDeck(name, isPublic, user.Id);
            return this.RedirectToAction("CreateCard", new { deckId = deck.Id });
        }

        public IActionResult CreateCard(string deckId)
        {
            return this.View("CreateCard", deckId);
        }

        public async Task<IActionResult> DeleteDeck(string id)
        {
            await this.decksService.DeleteDeck(id);
            return this.RedirectToAction("MyDecks");
        }
    }
}
