namespace CourseSystem.Services.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IDecksService
    {
        Task CreateDeck(string name, string isPublic, string userId);
    }
}
