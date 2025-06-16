namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class TrafficSignViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string SoundPath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Progress { get; set; } = 0;
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
} 