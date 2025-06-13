using System;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class MessageViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string Preview { get; set; } = string.Empty;
    }
} 