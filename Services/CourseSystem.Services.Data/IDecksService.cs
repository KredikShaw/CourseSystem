namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;

    public interface IDecksService
    {
        Task<Deck> CreateDeck(string name, string isPublic, string userId, string thumbnailUrl);

        IEnumerable<T> GetDecks<T>(string userId);

        Task DeleteDeck(string id);

        Task CreateCard(string frontSide, string backSide, string deckId);

        IEnumerable<T> GetCards<T>(string deckId);

        T GetCard<T>(string cardId);

        Task DeleteCard(string id);

        Task UpdateCard(string frontSide, string backSide, string id);
    }
}
