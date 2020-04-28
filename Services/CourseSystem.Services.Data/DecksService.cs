namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class DecksService : IDecksService
    {
        private readonly IDeletableEntityRepository<Deck> decksRepository;

        public DecksService(IDeletableEntityRepository<Deck> decksRepository)
        {
            this.decksRepository = decksRepository;
        }

        public async Task<Deck> CreateDeck(string name, string isPublic, string userId, string thumbnailUrl)
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
                Thumbnail = thumbnailUrl,
                UserId = userId,
            };

            await this.decksRepository.AddAsync(deck);
            await this.decksRepository.SaveChangesAsync();

            return deck;
        }

        public async Task DeleteDeck(string id, string userId)
        {
            var deck = this.decksRepository.All().FirstOrDefault(x => x.Id == id);
            if (deck.UserId == userId)
            {
                this.decksRepository.Delete(deck);
                await this.decksRepository.SaveChangesAsync();
            }
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
