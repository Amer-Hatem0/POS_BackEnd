using BRIXEL_core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BRIXEL_infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<SupportArticle> SupportArticles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<CompanyContactInfo> CompanyContactInfos { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AdvertisementMedia> AdvertisementMedia { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<AboutSection> AboutSection { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<WhyChooseUsSection> WhyChooseUsSection { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ===== Identity tables (موجودة عندك lowercase) =====
            builder.Entity<ApplicationUser>().ToTable("aspnetusers");
            builder.Entity<IdentityRole>().ToTable("aspnetroles");
            builder.Entity<IdentityUserRole<string>>().ToTable("aspnetuserroles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("aspnetuserclaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("aspnetroleclaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("aspnetuserlogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("aspnetusertokens");

            // ===== Domain tables (طابق الأسماء تمامًا كما في القاعدة) =====
            builder.Entity<AboutSection>().ToTable("AboutSection");
            builder.Entity<Advertisement>().ToTable("Advertisements");
            builder.Entity<AdvertisementMedia>().ToTable("advertisementmedia");
            builder.Entity<Category>().ToTable("categories");
            builder.Entity<CompanyContactInfo>().ToTable("companycontactinfos");
            builder.Entity<ContactMessage>().ToTable("contactmessages");
            builder.Entity<FAQ>().ToTable("faqs");
            builder.Entity<MediaFile>().ToTable("mediafiles");
            builder.Entity<PageContent>().ToTable("pagecontents");
            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<ProjectImage>().ToTable("ProjectImages");
            builder.Entity<Service>().ToTable("Services");
            builder.Entity<SupportArticle>().ToTable("supportarticles");
            builder.Entity<TeamMember>().ToTable("teammembers");
            builder.Entity<Testimonial>().ToTable("Testimonials");
            builder.Entity<WhyChooseUsSection>().ToTable("WhyChooseUsSection");

            // ===== إعداداتك كما هي =====
            builder.Entity<AboutSection>().Property(e => e.ServicesEnJson).HasColumnType("longtext");
            builder.Entity<AboutSection>().Property(e => e.ServicesArJson).HasColumnType("longtext");

            builder.Entity<Project>()
                .HasMany(p => p.ProjectImages)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectId);

            builder.Entity<WhyChooseUsSection>()
                .Property(e => e.BulletPointsEn).HasColumnType("longtext");
            builder.Entity<WhyChooseUsSection>()
                .Property(e => e.BulletPointsAr).HasColumnType("longtext");

            builder.Entity<Advertisement>()
                .HasOne(ad => ad.CreatedBy)
                .WithMany(user => user.Advertisements)
                .HasForeignKey(ad => ad.CreatedById)
                .OnDelete(DeleteBehavior.Restrict); // مطابق للـ FK عندك

            builder.Entity<Advertisement>()
                .HasOne(a => a.Category)
                .WithMany()
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Advertisement>()
                .HasMany(a => a.MediaFiles)
                .WithOne(m => m.Advertisement)
                .HasForeignKey(m => m.AdvertisementId);

            builder.Entity<Service>()
                .Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Entity<ContactMessage>()
                .Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Entity<CompanyContactInfo>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Entity<CompanyContactInfo>()
                .HasIndex(c => c.Email)
                .IsUnique();

            builder.Entity<FAQ>()
                .Property(f => f.DisplayOrder)
                .HasDefaultValue(0);

            builder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Project>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
