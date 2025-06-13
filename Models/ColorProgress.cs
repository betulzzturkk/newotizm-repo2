using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class ColorProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ColorName { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int Progress { get; set; } = 0;

        public int InteractionCount { get; set; } = 0;

        public DateTime LastInteraction { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
} 