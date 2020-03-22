namespace CourseSystem.Web.ViewModels.Decks
{
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class EditDeckCardViewModel : IMapFrom<Card>
    {
        public string Id { get; set; }

        public string DeckId { get; set; }

        public string FrontSide { get; set; }

        public string BackSide { get; set; }
    }
}
