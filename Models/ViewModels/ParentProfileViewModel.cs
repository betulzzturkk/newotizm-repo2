using System;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ParentProfileViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ChildName { get; set; } = string.Empty;
        public int ChildAge { get; set; }
        public string Interests { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = "/images/profile/default.jpg";
        public List<CourseViewModel> ActiveCourses { get; set; } = new();
        public DateTime LastProgressUpdate { get; set; } = DateTime.Now;
        public List<ActivityViewModel> RecentActivities { get; set; } = new();
        public List<MessageViewModel> RecentMessages { get; set; } = new();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<CourseViewModel> RecommendedCourses { get; set; } = new();
        public List<ChildViewModel> Children { get; set; } = new();
    }

    public class ActivityViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }

    public class ChildViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Interests { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public string? SpecialNeeds { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Notes { get; set; }
    }
}