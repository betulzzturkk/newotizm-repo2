using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class Child
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string? Diagnosis { get; set; }

    public string? Interests { get; set; }

    public string? SpecialNeeds { get; set; }

    public string ParentId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual AspNetUser Parent { get; set; } = null!;

    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}
