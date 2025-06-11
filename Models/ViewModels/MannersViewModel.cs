namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class MannersViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // Ev, Okul, Sosyal Alan vb.
        public string Color { get; set; } = string.Empty; // Bootstrap renk sınıfı
        public string Example { get; set; } = string.Empty; // Örnek durum
        public string CorrectBehavior { get; set; } = string.Empty; // Doğru davranış
    }
} 