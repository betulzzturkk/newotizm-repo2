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
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<AutismEducationPlatform.Web.Models.AnimalProgress> AnimalProgress { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ColorProgress> ColorProgress { get; set; }
        public DbSet<ShapeProgress> ShapeProgress { get; set; }
        public DbSet<NumberProgress> NumberProgress { get; set; }
        public DbSet<TrafficSignProgress> TrafficSignProgress { get; set; }
        public DbSet<Tale> Tales { get; set; }
        public DbSet<TaleProgress> TaleProgress { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<ExamAnswer> ExamAnswers { get; set; }
        public DbSet<MannerProgress> MannerProgress { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TrafficSign> TrafficSigns { get; set; }
        public DbSet<CourseProgress> CourseProgress { get; set; }

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
                entity.Property(e => e.DifficultyLevel).IsRequired();
                entity.Property(e => e.ImageUrl).HasMaxLength(200);
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

            builder.Entity<ActivityLog>(entity =>
            {
                entity.HasOne(e => e.Activity)
                    .WithMany()
                    .HasForeignKey(e => e.ActivityId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<AnimalProgress>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.UserId, e.AnimalId })
                    .IsUnique();
            });

            // Announcement için ilişki konfigürasyonu
            builder.Entity<Announcement>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // TrafficSign modeline SoundPath alanı eklendi (migration için not)

            // Child modeli için konfigürasyon
            builder.Entity<Child>()
                .HasOne(c => c.Parent)
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            // CourseProgress modeli için konfigürasyon
            builder.Entity<CourseProgress>()
                .HasOne(cp => cp.User)
                .WithMany()
                .HasForeignKey(cp => cp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CourseProgress>()
                .HasOne(cp => cp.Course)
                .WithMany()
                .HasForeignKey(cp => cp.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 