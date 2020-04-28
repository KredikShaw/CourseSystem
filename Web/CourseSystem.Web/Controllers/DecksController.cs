namespace CourseSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Decks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DecksController : Controller // TODO: Take the Card logic out of the deck logic and into it's own controller, service...
    {
        private readonly IDecksService decksService;
        private readonly ICardsService cardsService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Random random;

        public DecksController(
            IDecksService decksService,
            ICardsService cardsService,
            ICoursesService coursesService,
            UserManager<ApplicationUser> userManager,
            Random random)
        {
            this.decksService = decksService;
            this.cardsService = cardsService;
            this.coursesService = coursesService;
            this.userManager = userManager;
            this.random = random;
        }

        [Authorize]
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

        [Authorize]
        public IActionResult CreateDeck()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateDeck(DeckInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var thumbnailUrl = string.Empty;
            if (inputModel.Thumbnail != null)
            {
                thumbnailUrl = this.coursesService.UploadImageToCloudinary(inputModel.Thumbnail.OpenReadStream());
            }

            var deck = await this.decksService.CreateDeck(inputModel.Name, inputModel.IsPublic, user.Id, thumbnailUrl);
            return this.RedirectToAction("CreateCard", "Cards", new { deckId = deck.Id });
        }

        [Authorize]
        public async Task<IActionResult> DeleteDeck(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.decksService.DeleteDeck(id, userId);
            return this.RedirectToAction("MyDecks");
        }

        [Authorize]
        public IActionResult PlayDeck(string deckId, string passed, string npassed, string nfailed)
        {
            int numberOfPassed = 0;
            int numberOfFailed = 0;
            if (npassed != null && nfailed != null)
            {
                numberOfPassed = int.Parse(npassed);
                numberOfFailed = int.Parse(nfailed);
            }

            if (passed == "yes")
            {
                numberOfPassed++;
            }
            else if (passed == "no")
            {
                numberOfFailed++;
            }

            var allCards = this.cardsService.GetCards<CardViewModel>(deckId).ToList();

            var randomIndex = this.random.Next(0, allCards.Count);

            var card = allCards[randomIndex];
            card.Passed = numberOfPassed;
            card.Failed = numberOfFailed;
            return this.View(card);
        }

        [Authorize]
        public IActionResult EditDeck(string id)
        {
            var viewModel = new EditDeckViewModel
            {
                DeckId = id,
                Cards = this.cardsService.GetCards<EditDeckCardViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
