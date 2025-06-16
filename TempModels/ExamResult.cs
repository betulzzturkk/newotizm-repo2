using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class ExamResult
{
    public int Id { get; set; }

    public int ExamLevel { get; set; }

    public string UserId { get; set; } = null!;

    public int CorrectCount { get; set; }

    public int WrongCount { get; set; }

    public int TotalQuestions { get; set; }

    public double Score { get; set; }

    public DateTime CompletedAt { get; set; }

    public virtual ICollection<ExamAnswer> ExamAnswers { get; set; } = new List<ExamAnswer>();

    public virtual AspNetUser User { get; set; } = null!;
}
