using System;
using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models
{
    public class Tale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string VideoUrl { get; set; }

        [Required]
        public string ThumbnailUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 