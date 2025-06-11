using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutismEducationPlatform.Web.Models;

namespace AutismEducationPlatform.Web.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Rolleri oluştur
            string[] roles = { "Parent", "Instructor" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Örnek kullanıcılar oluştur
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Instructor");
                }
            }

            var parentUser = await userManager.FindByEmailAsync("parent@example.com");
            if (parentUser == null)
            {
                parentUser = new ApplicationUser
                {
                    UserName = "parent@example.com",
                    Email = "parent@example.com",
                    EmailConfirmed = true,
                    FirstName = "Parent",
                    LastName = "User"
                };

                var result = await userManager.CreateAsync(parentUser, "Parent123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(parentUser, "Parent");
                }
            }
        }
    }
} 