using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [Range(3, 18)]
        public int Age { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Diagnosis { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Hobbies { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        [StringLength(450)]
        public string? InstructorId { get; set; }

        public virtual ApplicationUser? Instructor { get; set; }

        public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
} 