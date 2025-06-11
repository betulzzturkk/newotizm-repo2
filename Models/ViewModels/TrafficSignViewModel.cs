namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class TrafficSignViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty; // Bootstrap renk sınıfı
        public string SignType { get; set; } = string.Empty; // Uyarı, Yasak, Bilgi vb.
        public string Example { get; set; } = string.Empty; // Gerçek hayattan örnek
    }
} 