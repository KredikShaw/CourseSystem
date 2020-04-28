namespace CourseSystem.Web.ViewModels.Decks
{
    using System.ComponentModel.DataAnnotations;

    public class CardInputModel
    {
        [Required(ErrorMessage = "Field is Required")]
        public string FrontSide { get; set; }

        [Required(ErrorMessage = "Field is Required")]
        public string BackSide { get; set; }

        public string DeckId { get; set; }

        public string SubmitType { get; set; }
    }
}
