using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class Activity
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int DurationMinutes { get; set; }

    public string Category { get; set; } = null!;

    public string Difficulty { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CompletionDate { get; set; }

    public bool IsCompleted { get; set; }

    public int Score { get; set; }

    public string Type { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual Course Course { get; set; } = null!;

    public virtual AspNetUser? User { get; set; }
}
