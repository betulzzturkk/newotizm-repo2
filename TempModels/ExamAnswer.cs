using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.TempModels;

public partial class ExamAnswer
{
    public int Id { get; set; }

    public int ExamResultId { get; set; }

    public int QuestionIndex { get; set; }

    public string QuestionText { get; set; } = null!;

    public string SelectedOption { get; set; } = null!;

    public string CorrectOption { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public virtual ExamResult ExamResult { get; set; } = null!;
}
