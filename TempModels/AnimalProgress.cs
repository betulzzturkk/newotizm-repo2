using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class AnimalProgress
{
    public int Id { get; set; }

    public int AnimalId { get; set; }

    public string UserId { get; set; } = null!;

    public int Progress { get; set; }

    public int InteractionCount { get; set; }

    public DateTime LastInteraction { get; set; }

    public DateTime CreatedAt { get; set; }

    public string AnimalName { get; set; } = null!;

    public DateTime LastUpdate { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
