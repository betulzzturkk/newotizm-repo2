using System.Collections.Generic;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ExamQuestionViewModel
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public int CorrectIndex { get; set; }
    }
} 