using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(1, 180)]
        public int DurationMinutes { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        [StringLength(50)]
        public string? Difficulty { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
} 