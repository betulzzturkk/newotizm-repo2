using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ParentProfileViewModel
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir.")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Çocuğun adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Çocuğun adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Çocuğun Adı")]
        public string ChildName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Çocuğun yaşı zorunludur.")]
        [Range(1, 18, ErrorMessage = "Çocuğun yaşı 1-18 arasında olmalıdır.")]
        [Display(Name = "Çocuğun Yaşı")]
        public int? ChildAge { get; set; }

        [StringLength(200, ErrorMessage = "Tanı en fazla 200 karakter olabilir.")]
        [Display(Name = "Tanı")]
        public string ChildDiagnosis { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir.")]
        [Display(Name = "Adres")]
        public string Address { get; set; } = string.Empty;
    }
} 