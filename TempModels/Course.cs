using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int DurationMinutes { get; set; }

    public string Category { get; set; } = null!;

    public string? InstructorId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string IconClass { get; set; } = null!;

    public string BackgroundColor { get; set; } = null!;

    public int DifficultyLevel { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int StudentCount { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual AspNetUser? Instructor { get; set; }

    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}
