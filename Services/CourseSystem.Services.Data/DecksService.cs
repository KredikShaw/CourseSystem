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
        private readonly IDeletableEntityRepository<Card> cardsRepository;

        public DecksService(IDeletableEntityRepository<Deck> decksRepository, IDeletableEntityRepository<Card> cardsRepository)
        {
            this.decksRepository = decksRepository;
            this.cardsRepository = cardsRepository;
        }

        public async Task CreateCard(string frontSide, string backSide, string deckId)
        {
            var card = new Card
            {
                FrontSide = frontSide,
                BackSide = backSide,
                DeckId = deckId,
            };

            await this.cardsRepository.AddAsync(card);
            await this.cardsRepository.SaveChangesAsync();
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

        public List<Card> GetAllCardsFromDeck(string deckId)
        {
            var cards = this.cardsRepository.All().Where(x => x.DeckId == deckId).ToList();
            return cards;
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
