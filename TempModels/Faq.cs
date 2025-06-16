using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class Faq
{
    public int Id { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public string Category { get; set; } = null!;

    public bool IsActive { get; set; }

    public int OrderIndex { get; set; }
}
