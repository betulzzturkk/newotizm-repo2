using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class ExamResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExamLevel { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int CorrectCount { get; set; }

        [Required]
        public int WrongCount { get; set; }

        [Required]
        public int TotalQuestions { get; set; }

        [Required]
        public double Score { get; set; } // Başarı yüzdesi

        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
} 