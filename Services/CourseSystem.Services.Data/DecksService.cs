namespace CourseSystem.Services.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class DecksService : IDecksService
    {
        private readonly IDeletableEntityRepository<Deck> decksRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public DecksService(IDeletableEntityRepository<Deck> decksRepository, UserManager<ApplicationUser> userManager)
        {
            this.decksRepository = decksRepository;
            this.userManager = userManager;
        }

        public async Task CreateDeck(string name, string isPublic, string userId)
        {
            bool isPub = true;

            if (isPublic == "Private")
            {
                isPub = false;
            }

            var deck = new Deck
            {
                Name = name,
                IsPublic = isPub,
                UserId = userId,
            };

            await this.decksRepository.AddAsync(deck);
            await this.decksRepository.SaveChangesAsync();
        }
    }
}
