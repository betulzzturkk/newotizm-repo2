using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class Information
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string Category { get; set; } = null!;

    public bool IsImportant { get; set; }

    public string? Tags { get; set; }
}
