using System;
using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models
{
    public class MannerProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MannerName { get; set; }
        public int Progress { get; set; }
        public int InteractionCount { get; set; }
        public DateTime LastInteraction { get; set; }
    }
} 