using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class ShapeProgress
{
    public int Id { get; set; }

    public string ShapeName { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int Progress { get; set; }

    public int InteractionCount { get; set; }

    public DateTime LastInteraction { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
