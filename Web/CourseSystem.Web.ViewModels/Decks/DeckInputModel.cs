namespace CourseSystem.Web.ViewModels.Decks
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class DeckInputModel
    {
        [Required(ErrorMessage = "Give your deck a name with at least 3 symbols")]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string IsPublic { get; set; }

        public IFormFile Thumbnail { get; set; }
    }
}
