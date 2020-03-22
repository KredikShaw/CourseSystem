namespace CourseSystem.Web.ViewModels.Decks
{
    public class CardViewModel
    {
        public string DeckName { get; set; }

        public string FrontSide { get; set; }

        public string BackSide { get; set; }

        public string DeckId { get; set; }

        public int Passed { get; set; }

        public int Failed { get; set; }
    }
}
