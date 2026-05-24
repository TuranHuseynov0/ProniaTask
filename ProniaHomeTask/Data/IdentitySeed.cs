using Microsoft.AspNetCore.Identity;
using ProniaHomeTask.Helpers;
using ProniaHomeTask.Models;

namespace ProniaHomeTask.Data
{
    public static class IdentitySeed
    {
        public const string AdminEmail = "admin@pronia.com";
        public const string AdminPassword = "Admin123!";

        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            foreach (var role in new[] { AppRoles.Admin, AppRoles.User })
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var admin = await userManager.FindByEmailAsync(AdminEmail);
            if (admin == null)
            {
                admin = new AppUser
                {
                    UserName = AdminEmail,
                    Email = AdminEmail,
                    FullName = "Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, AdminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }
            else if (!await userManager.IsInRoleAsync(admin, AppRoles.Admin))
            {
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }
        }
    }
}
