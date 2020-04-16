namespace CourseSystem.Web.ViewModels.Decks
{
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class DeckViewModel : IMapFrom<Deck>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public string Thumbnail { get; set; }

        public ApplicationUser User { get; set; }
    }
}
