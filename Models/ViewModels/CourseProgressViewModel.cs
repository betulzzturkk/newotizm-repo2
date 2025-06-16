using System;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class CourseProgressViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime LastInteraction { get; set; }
        public int CompletedActivities { get; set; }
        public int TotalActivities { get; set; }
        public bool IsCompleted => ProgressPercentage >= 100;
    }
} 