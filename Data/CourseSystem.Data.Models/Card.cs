namespace CourseSystem.Data.Models
{
    using System;

    using CourseSystem.Data.Common.Models;

    public class Card : BaseDeletableModel<string>
    {
        public Card()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FrontSide { get; set; }

        public string BackSide { get; set; }

        public string DeckId { get; set; }

        public Deck Deck { get; set; }
    }
}
