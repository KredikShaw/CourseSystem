namespace CourseSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Decks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DecksController : Controller // TODO: Take the Card logic out of the deck logic and into it's own controller, service...
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
        public async Task<IActionResult> CreateCard(string from, string deckId, string frontSide, string backSide, string submitType)
        {
            await this.decksService.CreateCard(frontSide, backSide, deckId);
            if (submitType == "another")
            {
                return this.RedirectToAction("CreateCard", new { deckId });
            }
            else
            {
                if (from == "EditDeck")
                {
                    return this.RedirectToAction("EditDeck", new { id = deckId });
                }

                return this.RedirectToAction("MyDecks");
            }
        }

        public async Task<IActionResult> DeleteDeck(string id)
        {
            await this.decksService.DeleteDeck(id);
            return this.RedirectToAction("MyDecks");
        }

        public async Task<IActionResult> DeleteCard(string id, string deckId)
        {
            await this.decksService.DeleteCard(id);
            return this.RedirectToAction("EditDeck", new { id = deckId });
        }

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

            var allCards = this.decksService.GetCards<CardViewModel>(deckId).ToList();

            var randomIndex = this.random.Next(0, allCards.Count);

            var card = allCards[randomIndex];
            card.Passed = numberOfPassed;
            card.Failed = numberOfFailed;
            return this.View(card);
        }

        public IActionResult EditDeck(string id)
        {
            var viewModel = new EditDeckViewModel
            {
                DeckId = id,
                Cards = this.decksService.GetCards<EditDeckCardViewModel>(id),
            };

            // TODO: Make difference when adding card from EditDeck
            return this.View(viewModel);
        }

        public IActionResult EditCard(string id)
        {
            var viewModel = this.decksService.GetCard<EditDeckCardViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCard(string frontSide, string backSide, string cardId, string deckId)
        {
            await this.decksService.UpdateCard(frontSide, backSide, cardId);
            return this.RedirectToAction("EditDeck", new { id = deckId });
        }
    }
}
