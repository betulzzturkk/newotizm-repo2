using System;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class FeedbackReport
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string ActivityName { get; set; } = string.Empty;
        public int Score { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }
} 