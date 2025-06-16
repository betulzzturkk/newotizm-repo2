using System;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ExamProgressViewModel
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public string ExamLevel { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
        public int Score { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public int TotalQuestions { get; set; }
    }
} 