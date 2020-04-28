namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICardsService
    {
        Task CreateCard(string frontSide, string backSide, string deckId);

        IEnumerable<T> GetCards<T>(string deckId);

        T GetCard<T>(string cardId);

        Task DeleteCard(string id, string userId);

        Task UpdateCard(string frontSide, string backSide, string id, string userId);
    }
}
