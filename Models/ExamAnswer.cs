using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class ExamAnswer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExamResultId { get; set; }

        [Required]
        public int QuestionIndex { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string SelectedOption { get; set; }

        [Required]
        public string CorrectOption { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("ExamResultId")]
        public ExamResult ExamResult { get; set; }
    }
} 