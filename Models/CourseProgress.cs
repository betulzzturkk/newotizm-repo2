using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class CourseProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        public int ProgressPercentage { get; set; }

        public DateTime LastInteraction { get; set; }

        public int CompletedActivities { get; set; }

        public int TotalActivities { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
} 