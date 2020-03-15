namespace CourseSystem.Web.Controllers
{
    using CourseSystem.Services.Data;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class DecksController : Controller
    {
        private readonly IDecksService decksService;

        public DecksController(IDecksService decksService)
        {
            this.decksService = decksService;
        }

        public IActionResult MyDecks()
        {
            return this.View("MyDecks");
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(string name, string isPublic)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.decksService.CreateDeck(name, isPublic, userId);
            return this.RedirectToAction("MyDecks");
        }
    }
}
