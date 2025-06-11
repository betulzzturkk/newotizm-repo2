using System;
using System.Collections.Generic;
using AutismEducationPlatform.Web.Models;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ParentDashboardViewModel
    {
        public ChildInfo ChildInfo { get; set; } = new();
        public List<CourseViewModel> Courses { get; set; } = new();
        public ProgressReport ProgressReport { get; set; } = new();
        public List<Announcement> Announcements { get; set; } = new();
        public List<Message> Messages { get; set; } = new();
        public List<FAQ> FAQs { get; set; } = new();
        public ContactInfo ContactInfo { get; set; } = new();
        public UserProfile UserProfile { get; set; } = new();
        public List<ProgressData> ProgressData { get; set; } = new();
    }

    public class ChildInfo
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Interests { get; set; } = string.Empty;
        public string SpecialNeeds { get; set; } = string.Empty;
    }

    public class Announcement
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }

    public class Message
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }

    public class FAQ
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }

    public class ContactInfo
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string SupportHours { get; set; } = string.Empty;
    }

    public class UserProfile
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
    }

    public class ProgressData
    {
        public string Category { get; set; } = string.Empty;
        public int Value { get; set; }
        public string Color { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
} 