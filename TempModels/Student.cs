using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string Diagnosis { get; set; } = null!;

    public string? Hobbies { get; set; }

    public string? Notes { get; set; }

    public string? InstructorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual AspNetUser? Instructor { get; set; }

    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}
