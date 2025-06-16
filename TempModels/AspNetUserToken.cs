using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class AspNetUserToken
{
    public string UserId { get; set; } = null!;

    public string LoginProvider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public string? ApplicationUserId { get; set; }

    public virtual AspNetUser? ApplicationUser { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
