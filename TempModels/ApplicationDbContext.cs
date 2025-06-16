using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AutismEducationPlatform.Web.TempModels;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<AnimalProgress> AnimalProgresses { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<ColorProgress> ColorProgresses { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<ExamAnswer> ExamAnswers { get; set; }

    public virtual DbSet<ExamResult> ExamResults { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Information> Informations { get; set; }

    public virtual DbSet<MannerProgress> MannerProgresses { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NumberProgress> NumberProgresses { get; set; }

    public virtual DbSet<Progress> Progresses { get; set; }

    public virtual DbSet<ShapeProgress> ShapeProgresses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<TaleProgress> TaleProgresses { get; set; }

    public virtual DbSet<TrafficSignProgress> TrafficSignProgresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasIndex(e => e.CourseId, "IX_Activities_CourseId");

            entity.HasIndex(e => e.UserId, "IX_Activities_UserId");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasDefaultValue("");
            entity.Property(e => e.Difficulty)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasDefaultValue("");

            entity.HasOne(d => d.Course).WithMany(p => p.Activities).HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.User).WithMany(p => p.Activities).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasIndex(e => e.ActivityId, "IX_ActivityLogs_ActivityId");

            entity.HasIndex(e => e.ChildId, "IX_ActivityLogs_ChildId");

            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Activity).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Child).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AnimalProgress>(entity =>
        {
            entity.ToTable("AnimalProgress");

            entity.HasIndex(e => new { e.UserId, e.AnimalId }, "IX_AnimalProgress_UserId_AnimalId").IsUnique();

            entity.Property(e => e.AnimalName).HasDefaultValue("");

            entity.HasOne(d => d.User).WithMany(p => p.AnimalProgresses).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Announcements_UserId");

            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.InstructorId, "IX_AspNetUsers_InstructorId");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.ChildDiagnosis).HasMaxLength(200);
            entity.Property(e => e.ChildName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.EmergencyContact).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.ParentName).HasMaxLength(100);
            entity.Property(e => e.PreferredLanguage).HasMaxLength(50);
            entity.Property(e => e.ProfilePicture).HasMaxLength(200);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Specialization).HasMaxLength(200);
            entity.Property(e => e.Surname).HasMaxLength(100);
            entity.Property(e => e.TimeZone).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Instructor).WithMany(p => p.InverseInstructor).HasForeignKey(d => d.InstructorId);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasIndex(e => e.ApplicationUserId, "IX_AspNetUserTokens_ApplicationUserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.ApplicationUser).WithMany(p => p.AspNetUserTokenApplicationUsers).HasForeignKey(d => d.ApplicationUserId);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokenUsers).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasIndex(e => e.ParentId, "IX_Children_ParentId");

            entity.Property(e => e.Diagnosis).HasMaxLength(500);
            entity.Property(e => e.Interests).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SpecialNeeds).HasMaxLength(1000);

            entity.HasOne(d => d.Parent).WithMany(p => p.Children).HasForeignKey(d => d.ParentId);
        });

        modelBuilder.Entity<ColorProgress>(entity =>
        {
            entity.ToTable("ColorProgress");

            entity.HasIndex(e => e.UserId, "IX_ColorProgress_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.ColorProgresses).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.InstructorId, "IX_Courses_InstructorId");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasDefaultValue("");
            entity.Property(e => e.ImageUrl).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Instructor).WithMany(p => p.Courses).HasForeignKey(d => d.InstructorId);
        });

        modelBuilder.Entity<ExamAnswer>(entity =>
        {
            entity.HasIndex(e => e.ExamResultId, "IX_ExamAnswers_ExamResultId");

            entity.HasOne(d => d.ExamResult).WithMany(p => p.ExamAnswers).HasForeignKey(d => d.ExamResultId);
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_ExamResults_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.ExamResults).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.ToTable("FAQs");

            entity.Property(e => e.Answer).HasMaxLength(2000);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Question).HasMaxLength(500);
        });

        modelBuilder.Entity<Information>(entity =>
        {
            entity.ToTable("Information");
        });

        modelBuilder.Entity<MannerProgress>(entity =>
        {
            entity.ToTable("MannerProgress");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Messages_UserId");

            entity.Property(e => e.Content).HasMaxLength(2000);
            entity.Property(e => e.SenderName).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.Messages).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasIndex(e => e.AuthorId, "IX_News_AuthorId");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Content).HasMaxLength(2000);
            entity.Property(e => e.ImageUrl).HasMaxLength(200);
            entity.Property(e => e.Summary).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Author).WithMany(p => p.News).HasForeignKey(d => d.AuthorId);
        });

        modelBuilder.Entity<NumberProgress>(entity =>
        {
            entity.ToTable("NumberProgress");

            entity.HasIndex(e => e.UserId, "IX_NumberProgress_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.NumberProgresses).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Progress>(entity =>
        {
            entity.HasIndex(e => e.ChildId, "IX_Progresses_ChildId");

            entity.HasIndex(e => e.CourseId, "IX_Progresses_CourseId");

            entity.HasIndex(e => e.StudentId, "IX_Progresses_StudentId");

            entity.Property(e => e.Notes).HasMaxLength(1000);

            entity.HasOne(d => d.Child).WithMany(p => p.Progresses).HasForeignKey(d => d.ChildId);

            entity.HasOne(d => d.Course).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Student).WithMany(p => p.Progresses).HasForeignKey(d => d.StudentId);
        });

        modelBuilder.Entity<ShapeProgress>(entity =>
        {
            entity.ToTable("ShapeProgress");

            entity.HasIndex(e => e.UserId, "IX_ShapeProgress_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.ShapeProgresses).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.InstructorId, "IX_Students_InstructorId");

            entity.Property(e => e.Diagnosis).HasMaxLength(500);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Hobbies).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.Instructor).WithMany(p => p.Students).HasForeignKey(d => d.InstructorId);
        });

        modelBuilder.Entity<TaleProgress>(entity =>
        {
            entity.ToTable("TaleProgress");

            entity.HasIndex(e => e.UserId, "IX_TaleProgress_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.TaleProgresses).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<TrafficSignProgress>(entity =>
        {
            entity.ToTable("TrafficSignProgress");

            entity.HasIndex(e => e.UserId, "IX_TrafficSignProgress_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.TrafficSignProgresses).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
