using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class News
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string Category { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public bool IsActive { get; set; }

    public string? AuthorId { get; set; }

    public virtual AspNetUser? Author { get; set; }
}
