using System;
using System.Collections.Generic;
using AutismEducationPlatform.Web.Models;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class InstructorDashboardViewModel
    {
        public InstructorInfo InstructorInfo { get; set; } = new();
        public List<CourseViewModel> AssignedCourses { get; set; } = new();
        public List<StudentProgress> StudentProgress { get; set; } = new();
        public List<Information> InformationItems { get; set; } = new();
        public List<FeedbackReport> FeedbackReports { get; set; } = new();
    }

    public class InstructorInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Expertise { get; set; } = string.Empty;
    }

    public class StudentProgress
    {
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public int ProgressPercentage { get; set; }
        public DateTime? LastActivity { get; set; }
        public string StudentId { get; set; } = string.Empty;
    }

    public class ActivityProgress
    {
        public string ActivityName { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public int Score { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? Notes { get; set; }
    }

    public class Statistics
    {
        public int TotalStudents { get; set; }
        public int ActiveCourses { get; set; }
        public int CompletedCourses { get; set; }
        public double AverageProgress { get; set; }
        public int TotalActivities { get; set; }
        public int CompletedActivities { get; set; }
    }
} 