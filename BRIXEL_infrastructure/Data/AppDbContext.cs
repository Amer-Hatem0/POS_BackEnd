using BRIXEL_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
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
                .OnDelete(DeleteBehavior.Cascade);


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
