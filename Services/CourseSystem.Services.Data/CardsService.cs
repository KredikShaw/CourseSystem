namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CourseSystem.Data.Common.Repositories;
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class CardsService : ICardsService
    {
        private readonly IDeletableEntityRepository<Card> cardsRepository;
        private readonly IDeletableEntityRepository<Deck> decksRepository;

        public CardsService(IDeletableEntityRepository<Card> cardsRepository, IDeletableEntityRepository<Deck> decksRepository)
        {
            this.cardsRepository = cardsRepository;
            this.decksRepository = decksRepository;
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

        public async Task DeleteCard(string id, string userId)
        {
            var card = this.cardsRepository.All().FirstOrDefault(x => x.Id == id);
            var deck = this.decksRepository.All().FirstOrDefault(x => x.Id == card.DeckId);
            if (deck.UserId == userId)
            {
                this.cardsRepository.Delete(card);
                await this.cardsRepository.SaveChangesAsync();
            }
        }

        public T GetCard<T>(string cardId)
        {
            var card = this.cardsRepository.All()
                .Where(x => x.Id == cardId)
                .To<T>()
                .FirstOrDefault();

            return card;
        }

        public IEnumerable<T> GetCards<T>(string deckId)
        {
            var cards = this.cardsRepository.All()
                .Where(x => x.DeckId == deckId)
                .OrderBy(x => x.FrontSide)
                .To<T>()
                .ToList();

            return cards;
        }

        public async Task UpdateCard(string frontSide, string backSide, string id, string userId)
        {
            var card = this.cardsRepository.All()
                .FirstOrDefault(x => x.Id == id);

            var deck = this.decksRepository.All().FirstOrDefault(x => x.Id == card.DeckId);
            if (deck.UserId == userId)
            {
                card.FrontSide = frontSide;
                card.BackSide = backSide;

                this.cardsRepository.Update(card);
                await this.cardsRepository.SaveChangesAsync();
            }
        }
    }
}
