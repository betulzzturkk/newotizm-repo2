using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class MannerProgress
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string MannerName { get; set; } = null!;

    public int Progress { get; set; }

    public int InteractionCount { get; set; }

    public DateTime LastInteraction { get; set; }
}
