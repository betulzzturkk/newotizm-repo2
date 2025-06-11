namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ColorViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty; // Rengin hex kodu
        public string Example { get; set; } = string.Empty; // Örnek: "Kırmızı elma", "Mavi gökyüzü" gibi
    }
} 