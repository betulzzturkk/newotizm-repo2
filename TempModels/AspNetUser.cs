using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Surname { get; set; }

    public string? Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public bool IsActive { get; set; }

    public string? ChildName { get; set; }

    public int? ChildAge { get; set; }

    public string? ChildDiagnosis { get; set; }

    public string? Address { get; set; }

    public string Name { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public string? Specialization { get; set; }

    public int? Experience { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string? EmergencyContact { get; set; }

    public string? InstructorId { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public bool NotificationsEnabled { get; set; }

    public string? ParentName { get; set; }

    public string? PreferredLanguage { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string? TimeZone { get; set; }

    public string? ProfileImageUrl { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<AnimalProgress> AnimalProgresses { get; set; } = new List<AnimalProgress>();

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokenApplicationUsers { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokenUsers { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<ColorProgress> ColorProgresses { get; set; } = new List<ColorProgress>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

    public virtual AspNetUser? Instructor { get; set; }

    public virtual ICollection<AspNetUser> InverseInstructor { get; set; } = new List<AspNetUser>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<NumberProgress> NumberProgresses { get; set; } = new List<NumberProgress>();

    public virtual ICollection<ShapeProgress> ShapeProgresses { get; set; } = new List<ShapeProgress>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<TaleProgress> TaleProgresses { get; set; } = new List<TaleProgress>();

    public virtual ICollection<TrafficSignProgress> TrafficSignProgresses { get; set; } = new List<TrafficSignProgress>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
