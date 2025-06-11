using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class Child
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 18)]
        public int Age { get; set; }

        [StringLength(500)]
        public string? Diagnosis { get; set; }

        [StringLength(500)]
        public string? Interests { get; set; }

        [StringLength(1000)]
        public string? SpecialNeeds { get; set; }

        [Required]
        public string ParentId { get; set; } = string.Empty;

        [ForeignKey("ParentId")]
        public ApplicationUser? Parent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
} 