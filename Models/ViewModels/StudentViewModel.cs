using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [Display(Name = "Ad")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [Display(Name = "Soyad")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yaş alanı zorunludur.")]
        [Range(3, 18, ErrorMessage = "Yaş 3-18 arasında olmalıdır.")]
        [Display(Name = "Yaş")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Cinsiyet seçimi zorunludur.")]
        [Display(Name = "Cinsiyet")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tanı alanı zorunludur.")]
        [Display(Name = "Tanı")]
        public string Diagnosis { get; set; } = string.Empty;

        [Display(Name = "Hobiler")]
        public string? Hobbies { get; set; }

        [Display(Name = "Notlar")]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public InstructorInfo? InstructorInfo { get; set; }
    }
} 