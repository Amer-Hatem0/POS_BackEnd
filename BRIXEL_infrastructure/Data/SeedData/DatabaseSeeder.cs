using BRIXEL_core.Models;
using BRIXEL_infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BRIXEL_infrastructure.SeedData
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await context.Database.MigrateAsync();

            
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Publisher"))
                await roleManager.CreateAsync(new IdentityRole("Publisher"));


            if (!await userManager.Users.AnyAsync())
            {
                var admin = new ApplicationUser
                {
                    UserName = "brixelPs",
                    Email = "support@brixel.tech",
                    FullName = "Admin",
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    Console.WriteLine(" Admin user created.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($" Error: {error.Code} - {error.Description}");
                    }
                }
            }

        }
    }
}
