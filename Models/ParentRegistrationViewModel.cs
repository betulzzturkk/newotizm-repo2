using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models
{
    public class ParentRegistrationViewModel
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Çocuğun adı zorunludur.")]
        [Display(Name = "Çocuğun Adı")]
        public string ChildName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Çocuğun yaşı zorunludur.")]
        [Range(2, 18, ErrorMessage = "Çocuğun yaşı 2 ile 18 arasında olmalıdır.")]
        [Display(Name = "Çocuğun Yaşı")]
        public int ChildAge { get; set; }

        [Required(ErrorMessage = "Çocuğun cinsiyeti zorunludur.")]
        [Display(Name = "Çocuğun Cinsiyeti")]
        public string ChildGender { get; set; } = string.Empty;

        [Display(Name = "Özel Notlar")]
        public string SpecialNotes { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kullanım koşullarını kabul etmelisiniz.")]
        [Display(Name = "Kullanım Koşullarını Kabul Ediyorum")]
        public bool AcceptTerms { get; set; }
    }
} 