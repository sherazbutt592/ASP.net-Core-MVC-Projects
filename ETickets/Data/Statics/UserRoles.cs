using ETickets.Models;
using Microsoft.AspNetCore.Identity;

namespace ETickets.Data.Statics
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // ✅ 1. Seed Roles (no need for Task.Run or Wait)
            if (!await roleManager.RoleExistsAsync(Admin))
                await roleManager.CreateAsync(new IdentityRole(Admin));

            if (!await roleManager.RoleExistsAsync(User))
                await roleManager.CreateAsync(new IdentityRole(User));

            // ✅ 2. Seed Admin User
            string adminEmail = "admin@etickets.com";
            string adminPassword = "Admin@123"; // use a strong password to meet Identity rules

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var adminUser = new ApplicationUser
                {
                    FullName = "Admin User",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Admin);
                }
                else
                {
                    // optional logging for debugging
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"❌ Error creating admin: {error.Description}");
                    }
                }
            }
        }
    }
}
