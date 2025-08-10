using System.Text;
using BRIXEL.Middlewares;
using BRIXEL_core.Interface;
using BRIXEL_core.Models;
using BRIXEL_infrastructure.Data;
using BRIXEL_infrastructure.Repositories;
using BRIXEL_infrastructure.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BRIXEL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string: من appsettings أو من Environment (Render)
            var conn =
                builder.Configuration.GetConnectionString("DefaultConnection") ??
                Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            if (string.IsNullOrWhiteSpace(conn))
                throw new InvalidOperationException("Database connection string is missing.");

            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseMySql(conn, ServerVersion.AutoDetect(conn), sql =>
                {
                    sql.CommandTimeout(120);
                })
                .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
                .EnableDetailedErrors(builder.Environment.IsDevelopment())
            );

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Repos/Services (DI)
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICompanyContactService, CompanyContactService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IFAQService, FAQService>();
            builder.Services.AddScoped<IPageContentService, PageContentService>();
            builder.Services.AddScoped<ITestimonialService, TestimonialService>();
            builder.Services.AddScoped<IAboutSectionRepository, AboutSectionRepository>();
            builder.Services.AddScoped<IWhyChooseUsRepository, WhyChooseUsRepository>();

            // JWT
            var jwtKey = builder.Configuration["Jwt:Key"] ?? Environment.GetEnvironmentVariable("Jwt__Key");
            var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? Environment.GetEnvironmentVariable("Jwt__Issuer");
            var jwtAudience = builder.Configuration["Jwt:Audience"] ?? Environment.GetEnvironmentVariable("Jwt__Audience");

            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new InvalidOperationException("Jwt:Key is missing.");

            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = !string.IsNullOrWhiteSpace(jwtIssuer),
                    ValidIssuer = jwtIssuer,
                    ValidateAudience = !string.IsNullOrWhiteSpace(jwtAudience),
                    ValidAudience = jwtAudience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:5173",
                        "http://localhost:5000",
                        "http://localhost:8080"
                    // أضف دومين الفرونت بعد النشر: "https://your-frontend-domain.com"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            // حدود رفع الملفات (للصور)
            builder.Services.Configure<FormOptions>(o =>
            {
                o.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50MB
            });

            // Forwarded headers (خلف بروكسي مثل Render)
            builder.Services.Configure<ForwardedHeadersOptions>(o =>
            {
                o.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                o.KnownNetworks.Clear();
                o.KnownProxies.Clear();
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // تشغيل Seeder فقط في Development أو لو SEED_DB=true
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var env = services.GetRequiredService<IHostEnvironment>();
                var seedFlag = Environment.GetEnvironmentVariable("SEED_DB");

                if (env.IsDevelopment() || string.Equals(seedFlag, "true", StringComparison.OrdinalIgnoreCase))
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await DatabaseSeeder.SeedAsync(context, userManager, roleManager);
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseForwardedHeaders();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowFrontend");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGlobalExceptionHandler();

            app.MapGet("/ping", () => Results.Ok("pong"));
            app.MapControllers();

            app.Run();
        }
    }
}
