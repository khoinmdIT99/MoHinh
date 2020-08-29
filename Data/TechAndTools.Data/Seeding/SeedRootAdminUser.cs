namespace TechAndTools.Data.Seeding
{
    using Commons.Constants;
    using Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeedRootAdminUser : ISeeder
    {
        public async Task SeedAsync(TechAndToolsDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<TechAndToolsUser>>();

            var hasAdmin = userManager.GetUsersInRoleAsync(GlobalConstants.AdminRole).Result.Any();

            if (!hasAdmin)
            {
                TechAndToolsUser admin = new TechAndToolsUser
                {
                    UserName = GlobalConstants.AdminUsername,
                    FirstName = GlobalConstants.AdminFirstName,
                    LastName = GlobalConstants.AdminLastName,
                    Email = GlobalConstants.AdminEmail,
                    CreatedOn = DateTime.UtcNow,
                    PhoneNumber = GlobalConstants.AdminPhoneNumber,
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    ShoppingCart = new ShoppingCart()
                };

                var password = GlobalConstants.AdminPassword;

                await userManager.CreateAsync(admin, password);

                await userManager.AddToRoleAsync(admin, GlobalConstants.AdminRole);
            }
        }
    }
}
