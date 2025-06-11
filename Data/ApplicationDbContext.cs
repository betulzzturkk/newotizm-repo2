using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AutismEducationPlatform.Web.Models;

namespace AutismEducationPlatform.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Configure AspNetUsers table
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.Name).HasMaxLength(100);
                entity.Property(u => u.ChildName).HasMaxLength(100);
                entity.Property(u => u.Specialization).HasMaxLength(200);
            });

            // Configure Courses table
            builder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.Difficulty).HasMaxLength(50);
            });

            // Configure Activities table
            builder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.Difficulty).HasMaxLength(50);
            });

            // Configure Progress table
            builder.Entity<Progress>(entity =>
            {
                entity.HasOne(p => p.Course)
                    .WithMany()
                    .HasForeignKey(p => p.CourseId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Configure Children table
            builder.Entity<Child>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Diagnosis).HasMaxLength(500);
                entity.Property(e => e.Interests).HasMaxLength(500);
                entity.Property(e => e.SpecialNeeds).HasMaxLength(1000);
            });

            // Configure News table
            builder.Entity<News>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Summary).IsRequired().HasMaxLength(500);
            });
        }
    }
} 