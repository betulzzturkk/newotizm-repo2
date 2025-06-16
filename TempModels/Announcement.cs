using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class Announcement
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Type { get; set; } = null!;

    public bool IsRead { get; set; }

    public bool IsActive { get; set; }

    public string? UserId { get; set; }

    public virtual AspNetUser? User { get; set; }
}
