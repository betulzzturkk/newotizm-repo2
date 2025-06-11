using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ProgressReport
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int CompletedActivities { get; set; }
        public int SuccessRate { get; set; }
        public double TotalStudyTime { get; set; }
        public List<WeeklyProgress> WeeklyProgress { get; set; } = new();
        public List<CategoryProgress> CategoryProgress { get; set; } = new();
        public List<LearningActivityLog> RecentActivities { get; set; } = new();
        public OverallStatistics Statistics { get; set; } = new();
    }

    public class WeeklyProgress
    {
        public DateTime WeekStartDate { get; set; }
        public int CompletedLessons { get; set; }
        public double StudyTime { get; set; }
        public int SuccessRate { get; set; }
    }

    public class CategoryProgress
    {
        public string CategoryName { get; set; } = string.Empty;
        public int TotalLessons { get; set; }
        public int CompletedLessons { get; set; }
        public int SuccessRate { get; set; }
        public string Color { get; set; } = string.Empty;
    }

    public class LearningActivityLog
    {
        public DateTime Date { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Score { get; set; }
        public double Duration { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class OverallStatistics
    {
        public int TotalLessonsAvailable { get; set; }
        public int TotalLessonsCompleted { get; set; }
        public double AverageSuccessRate { get; set; }
        public double TotalStudyTimeHours { get; set; }
        public int ConsecutiveDays { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int TotalPoints { get; set; }
        public string CurrentLevel { get; set; } = string.Empty;
    }
} 