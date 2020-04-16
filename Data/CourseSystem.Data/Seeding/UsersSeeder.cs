namespace CourseSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CourseSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync("admin@courses.com");
            if (user != null)
            {
                return;
            }

            await userManager.CreateAsync(
                new ApplicationUser
            {
                UserName = "admin@courses.com",
                Email = "admin@courses.com",
                EmailConfirmed = true,
            }, "Adminpass2020");

            var createdUser = await userManager.FindByNameAsync("admin@courses.com");
            var role = await roleManager.FindByNameAsync("Administrator");

            await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = createdUser.Id,
            });
        }
    }
}
