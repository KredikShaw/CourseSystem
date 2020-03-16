namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;

    public interface IDecksService
    {
        Task CreateDeck(string name, string isPublic, string userId);

        IEnumerable<T> GetDecks<T>(string userId);
    }
}
