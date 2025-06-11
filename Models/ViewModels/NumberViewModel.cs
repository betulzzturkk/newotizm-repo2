namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class NumberViewModel
    {
        public int Value { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Example { get; set; } = string.Empty; // Örnek: "2 elma", "3 kuş" gibi
    }
} 