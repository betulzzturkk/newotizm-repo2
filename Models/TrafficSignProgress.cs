using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class TrafficSignProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SignName { get; set; }
        public int Progress { get; set; }

        [Required]
        public int TrafficSignId { get; set; }

        public int InteractionCount { get; set; } = 0;

        public DateTime LastInteraction { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
} 