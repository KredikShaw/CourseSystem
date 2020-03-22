namespace CourseSystem.Web.ViewModels.Decks
{
    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class CardViewModel : IMapFrom<Card>
    {
        public string DeckName { get; set; }

        public string FrontSide { get; set; }

        public string BackSide { get; set; }

        public string DeckId { get; set; }

        public int Passed { get; set; }

        public int Failed { get; set; }
    }
}
