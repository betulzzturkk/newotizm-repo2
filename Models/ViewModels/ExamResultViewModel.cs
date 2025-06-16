using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ExamResultViewModel
    {
        [Required]
        public string ExamLevel { get; set; }
        [Required]
        public int CorrectCount { get; set; }
        [Required]
        public int WrongCount { get; set; }
        [Required]
        public int TotalQuestions { get; set; }
        [Required]
        public double Score { get; set; }
        public DateTime CompletedAt { get; set; }

        public List<ExamAnswerInputModel> Answers { get; set; }
    }

    public class ExamAnswerInputModel
    {
        public string QuestionText { get; set; }
        public string SelectedOption { get; set; }
        public string CorrectOption { get; set; }
        public bool IsCorrect { get; set; }
    }
} 