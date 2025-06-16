using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class Message
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime Date { get; set; }

    public string SenderName { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public bool IsRead { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
