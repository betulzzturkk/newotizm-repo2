using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public int DurationMinutes { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        [StringLength(50)]
        public string? Difficulty { get; set; }

        [Required]
        public string? InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public ApplicationUser? Instructor { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [StringLength(200)]
        public string ImageUrl { get; set; } = "/images/courses/default.jpg";

        public virtual ICollection<Activity>? Activities { get; set; }

        public string Title { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
    }
} 