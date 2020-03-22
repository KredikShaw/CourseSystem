namespace CourseSystem.Web.ViewModels.Decks
{
    using System.Collections.Generic;

    public class EditDeckViewModel
    {
        public string DeckId { get; set; }

        public IEnumerable<EditDeckCardViewModel> Cards { get; set; }
    }
}
