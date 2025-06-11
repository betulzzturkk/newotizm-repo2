using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutismEducationPlatform.Web.Models
{
    public class Progress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChildId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [Range(0, 100)]
        public int CompletionPercentage { get; set; }

        [Required]
        public float StudyTimeHours { get; set; }

        [Required]
        [Range(0, 100)]
        public int SuccessRate { get; set; }

        public DateTime LastActivityDate { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("ChildId")]
        public Child? Child { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
} 