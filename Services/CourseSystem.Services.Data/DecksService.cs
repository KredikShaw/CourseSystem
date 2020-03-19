namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class DecksService : IDecksService
    {
        private readonly IDeletableEntityRepository<Deck> decksRepository;

        public DecksService(IDeletableEntityRepository<Deck> decksRepository)
        {
            this.decksRepository = decksRepository;
        }

        public async Task<Deck> CreateDeck(string name, string isPublic, string userId)
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

            return deck;
        }

        public async Task DeleteDeck(string id)
        {
            var deck = this.decksRepository.All().FirstOrDefault(x => x.Id == id);
            this.decksRepository.Delete(deck);
            await this.decksRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetDecks<T>(string userId)
        {
            var decks = this.decksRepository.All()
                .Where(x => x.IsPublic == true || x.UserId == userId)
                .OrderBy(x => x.UserId != userId)
                .ThenBy(x => x.Name)
                .To<T>()
                .ToList();

            return decks;
        }
    }
}
