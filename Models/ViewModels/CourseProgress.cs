using System;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class CourseProgress
    {
        public string CourseName { get; set; } = string.Empty;
        public double ProgressPercentage { get; set; }
        public DateTime LastAccessDate { get; set; }
        public int CompletedActivities { get; set; }
        public int TotalActivities { get; set; }
    }
} 