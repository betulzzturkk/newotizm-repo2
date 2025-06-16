using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ProgressViewModel
    {
        public string ChildName { get; set; }
        public int ChildAge { get; set; }
        public string ChildDiagnosis { get; set; }
        public DateTime LastActivity { get; set; }
        public List<CourseProgressViewModel> LastCompletedCourses { get; set; }
        public List<ExamProgressViewModel> Exams { get; set; }

        // View'da kullanılan hesaplanmış property'ler
        public int TotalCourses => LastCompletedCourses?.Count ?? 0;
        public int CompletedCourses => LastCompletedCourses?.Count(c => c.ProgressPercentage >= 100) ?? 0;
        public double AverageProgress => LastCompletedCourses?.Any() == true 
            ? Math.Round(LastCompletedCourses.Average(c => c.ProgressPercentage), 1) 
            : 0;
        public List<CourseProgressViewModel> Courses => LastCompletedCourses;

        // Eğitim İlerlemeleri
        public List<AnimalProgressViewModel> AnimalProgress { get; set; } = new List<AnimalProgressViewModel>();
        public List<ColorProgressViewModel> ColorProgress { get; set; } = new List<ColorProgressViewModel>();
        public List<ShapeProgressViewModel> ShapeProgress { get; set; } = new List<ShapeProgressViewModel>();
        public List<TaleProgressViewModel> TaleProgress { get; set; } = new List<TaleProgressViewModel>();
        public List<TrafficSignProgressViewModel> TrafficSignProgress { get; set; } = new List<TrafficSignProgressViewModel>();
        public List<MannerProgressViewModel> MannerProgress { get; set; } = new List<MannerProgressViewModel>();
        public List<NumberProgressViewModel> NumberProgress { get; set; } = new List<NumberProgressViewModel>();

        // İstatistikler
        public int TotalActivities { get; set; }
        public int CompletedActivities { get; set; }
    }

    public class AnimalProgressViewModel
    {
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime LastInteraction { get; set; }
    }

    public class ColorProgressViewModel
    {
        public string ColorName { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime LastInteraction { get; set; }
    }

    public class ShapeProgressViewModel
    {
        public string ShapeName { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime LastInteraction { get; set; }
    }

    public class TaleProgressViewModel
    {
        public int TaleId { get; set; }
        public string TaleTitle { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime LastInteraction { get; set; }
    }

    public class TrafficSignProgressViewModel
    {
        public int TrafficSignId { get; set; }
        public string SignName { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime LastInteraction { get; set; }
    }

    public class MannerProgressViewModel
    {
        public string MannerName { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime LastInteraction { get; set; }
    }

    public class NumberProgressViewModel
    {
        public int NumberValue { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime LastInteraction { get; set; }
    }
} 