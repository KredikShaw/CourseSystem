namespace CourseSystem.Web.ViewModels.Decks
{
    using System.ComponentModel.DataAnnotations;

    using CourseSystem.Data.Models;
    using CourseSystem.Services.Mapping;

    public class EditDeckCardViewModel : IMapFrom<Card>
    {
        public string Id { get; set; }

        public string DeckId { get; set; }

        [Required]
        public string FrontSide { get; set; }

        [Required]
        public string BackSide { get; set; }
    }
}
