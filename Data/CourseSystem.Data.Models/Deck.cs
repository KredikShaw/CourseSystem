namespace CourseSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CourseSystem.Data.Common.Models;

    public class Deck : BaseDeletableModel<string>
    {
        public Deck()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cards = new HashSet<Card>();
        }

        public string Name { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
