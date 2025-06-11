using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
        [Display(Name = "Ad Soyad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Şifre en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Rol seçimi zorunludur.")]
        public string Role { get; set; }

        // Veli için özel alanlar
        [Display(Name = "Çocuğun Adı")]
        public string ChildName { get; set; }

        [Display(Name = "Çocuğun Yaşı")]
        [Range(1, 18, ErrorMessage = "Yaş 1-18 arasında olmalıdır.")]
        public int? ChildAge { get; set; }

        // Eğitmen için özel alanlar
        [Display(Name = "Uzmanlık Alanı")]
        public string Specialization { get; set; }

        [Display(Name = "Deneyim (Yıl)")]
        [Range(0, 50, ErrorMessage = "Deneyim yılı 0-50 arasında olmalıdır.")]
        public int? Experience { get; set; }
    }
} 