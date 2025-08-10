using BRIXEL_core.Models;
using BRIXEL_infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BRIXEL_infrastructure.SeedData
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // مهم: لا تعمل Migrate/EnsureCreated هنا على الإنتاج
            // نتعامل مع جداول جاهزة من الـ dump
            await EnsureRolesAsync(roleManager);
            await EnsureAdminUserAsync(userManager, roleManager);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Publisher" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task EnsureAdminUserAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // نقرأ القيم من Environment Variables إن وُجدت
            var adminUserName = Environment.GetEnvironmentVariable("ADMIN_USERNAME") ?? "brixelPs";
            var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? "support@brixel.tech";
            var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "Admin@123";

            // ابحث باليوزرنيم أو بالإيميل
            var userByName = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == adminUserName);
            var userByMail = await userManager.FindByEmailAsync(adminEmail);
            var admin = userByName ?? userByMail;

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    FullName = "Admin",
                    EmailConfirmed = true,
                    IsActive = true
                };

                var createRes = await userManager.CreateAsync(admin, adminPassword);
                if (!createRes.Succeeded)
                    throw new Exception("Failed to create admin user: " + string.Join("; ", createRes.Errors.Select(e => $"{e.Code}:{e.Description}")));
            }

            // تأكيد إضافة الدور Admin
            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
