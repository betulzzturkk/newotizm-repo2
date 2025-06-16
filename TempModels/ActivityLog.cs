using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class ActivityLog
{
    public int Id { get; set; }

    public int ActivityId { get; set; }

    public int ChildId { get; set; }

    public int Score { get; set; }

    public int DurationMinutes { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual Child Child { get; set; } = null!;
}
