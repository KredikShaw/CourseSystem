namespace CourseSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<(string Name, string ImageUrl)>
            {
                ("Art", "https://res.cloudinary.com/alekskn99/image/upload/v1585341788/art_ysbfw8.jpg"),
                ("Beauty", "https://res.cloudinary.com/alekskn99/image/upload/v1585341787/beauty_kfpufm.jpg"),
                ("Business", "https://res.cloudinary.com/alekskn99/image/upload/v1585341788/business_flwekj.jpg"),
                ("Crafts", "https://res.cloudinary.com/alekskn99/image/upload/v1585341787/crafts_uxqbja.jpg"),
                ("Development", "https://res.cloudinary.com/alekskn99/image/upload/v1585341788/programming_bfqhtu.jpg"),
                ("Gardening", "https://res.cloudinary.com/alekskn99/image/upload/v1585341788/gardening_mvngch.jpg"),
                ("Language", "https://res.cloudinary.com/alekskn99/image/upload/v1585341787/language_bjw2nh.jpg"),
                ("Marketing", "https://res.cloudinary.com/alekskn99/image/upload/v1585341787/marketing_rmiakp.jpg"),
                ("Music", "https://res.cloudinary.com/alekskn99/image/upload/v1585341788/music_zpqx0u.jpg"),
                ("Other", "https://res.cloudinary.com/alekskn99/image/upload/v1585341788/other_eo4yr0.jpg"),
                ("Photography", "https://res.cloudinary.com/alekskn99/image/upload/v1585341788/photography_uib9zp.jpg"),
                ("Self-Improvement", "https://res.cloudinary.com/alekskn99/image/upload/v1585341789/selfimprovement_hhvq7a.jpg"),
                ("Sports", "https://res.cloudinary.com/alekskn99/image/upload/v1585341789/sports_pbijsg.jpg"),
            };
            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category.Name,
                    ImageUrl = category.ImageUrl,
                });
            }
        }
    }
}
