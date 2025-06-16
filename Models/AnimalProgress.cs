using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class AnimalProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AnimalId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string AnimalName { get; set; } = string.Empty;

        [Required]
        public int Progress { get; set; } = 0; // 0-100 arası

        public int InteractionCount { get; set; } = 0;

        public int CompletedAnimalCount { get; set; } = 0;

        public DateTime LastInteraction { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
} 