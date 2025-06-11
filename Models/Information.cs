using System;

namespace AutismEducationPlatform.Web.Models
{
    public class Information
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsImportant { get; set; }
        public string? Tags { get; set; }
    }
} 