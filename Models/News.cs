using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [MaxLength(500)]
        public string Summary { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string Category { get; set; }

        public DateTime PublishDate { get; set; } = DateTime.Now;

        public string? Source { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
} 