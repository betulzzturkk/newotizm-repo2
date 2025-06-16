using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class Progress
{
    public int Id { get; set; }

    public int ChildId { get; set; }

    public int CourseId { get; set; }

    public int CompletionPercentage { get; set; }

    public float StudyTimeHours { get; set; }

    public int SuccessRate { get; set; }

    public DateTime LastActivityDate { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? StudentId { get; set; }

    public virtual Child Child { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual Student? Student { get; set; }
}
