namespace CourseSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Data;
    using CourseSystem.Web.ViewModels.Decks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CardsController(ICardsService cardsService, UserManager<ApplicationUser> userManager)
        {
            this.cardsService = cardsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult CreateCard(string deckId)
        {
            var viewModel = new CardInputModel
            {
                DeckId = deckId,
            };

            return this.View("CreateCard", viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCard(CardInputModel inputModel, string submitType)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.cardsService.CreateCard(inputModel.FrontSide, inputModel.BackSide, inputModel.DeckId);
            if (submitType == "another")
            {
                return this.RedirectToAction("CreateCard", new { inputModel.DeckId });
            }
            else
            {
                return this.RedirectToAction("MyDecks", "Decks");
            }
        }

        public async Task<IActionResult> DeleteCard(string id, string deckId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.cardsService.DeleteCard(id, userId);
            return this.RedirectToAction("EditDeck", "Decks", new { id = deckId });
        }

        [Authorize]
        public IActionResult EditCard(string id)
        {
            var viewModel = this.cardsService.GetCard<EditDeckCardViewModel>(id);
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditCard(EditDeckCardViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.cardsService.UpdateCard(inputModel.FrontSide, inputModel.BackSide, inputModel.Id, userId);
            return this.RedirectToAction("EditDeck", "Decks", new { id = inputModel.DeckId });
        }
    }
}
