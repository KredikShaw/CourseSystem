namespace CourseSystem.Web.Controllers
{
    using System;
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
        private readonly Random random;

        public DecksController(
            IDecksService decksService,
            UserManager<ApplicationUser> userManager,
            Random random)
        {
            this.decksService = decksService;
            this.userManager = userManager;
            this.random = random;
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

        [HttpPost]
        public async Task<IActionResult> CreateCard(string deckId, string frontSide, string backSide, string submitType)
        {
            await this.decksService.CreateCard(frontSide, backSide, deckId);
            if (submitType == "another")
            {
                return this.RedirectToAction("CreateCard", new { deckId });
            }
            else
            {
                return this.RedirectToAction("MyDecks");
            }
        }

        public async Task<IActionResult> DeleteDeck(string id)
        {
            await this.decksService.DeleteDeck(id);
            return this.RedirectToAction("MyDecks");
        }

        public IActionResult PlayDeck(string deckId, string name)
        {
            var allCards = this.decksService.GetAllCardsFromDeck(deckId);

            var randomIndex = this.random.Next(0, allCards.Count - 1);

            var card = allCards[randomIndex];

            var viewModel = new CardViewModel
            {
                DeckId = deckId,
                FrontSide = card.FrontSide,
                BackSide = card.BackSide,
                DeckName = name,
            };

            return this.View(viewModel);
        }
    }
}
