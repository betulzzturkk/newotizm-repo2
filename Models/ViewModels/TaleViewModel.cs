using System;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class TaleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ImagePath { get; set; }
        public string SoundPath { get; set; }
        public int Progress { get; set; }
        public double ProgressPercentage { get; set; }
        public DateTime LastInteraction { get; set; }
    }
} 