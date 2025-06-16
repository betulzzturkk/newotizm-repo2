using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class ActivityLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ActivityId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Range(0, 100)]
        public int Score { get; set; }

        public int DurationMinutes { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [ForeignKey("ActivityId")]
        public Activity? Activity { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }
    }
} 