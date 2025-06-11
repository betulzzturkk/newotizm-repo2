using System;
using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
        public UserProfileInfo UserInfo { get; set; } = new();
        public ChildProfileInfo ChildInfo { get; set; } = new();
        public ContactInformation ContactInfo { get; set; } = new();
        public NotificationSettings NotificationSettings { get; set; } = new();
    }

    public class UserProfileInfo
    {
        [Required(ErrorMessage = "Ad Soyad gereklidir")]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Profil Fotoğrafı")]
        public string ProfilePicture { get; set; } = string.Empty;

        [Display(Name = "Kayıt Tarihi")]
        public DateTime RegistrationDate { get; set; }
    }

    public class ChildProfileInfo
    {
        [Required(ErrorMessage = "Çocuğun adı gereklidir")]
        [Display(Name = "Çocuğun Adı")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Doğum tarihi gereklidir")]
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Cinsiyet gereklidir")]
        [Display(Name = "Cinsiyet")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tanı gereklidir")]
        [Display(Name = "Tanı")]
        public string Diagnosis { get; set; } = string.Empty;

        [Display(Name = "Tanı Tarihi")]
        [DataType(DataType.Date)]
        public DateTime DiagnosisDate { get; set; }

        [Display(Name = "İlgi Alanları")]
        public string Interests { get; set; } = string.Empty;

        [Display(Name = "Özel İhtiyaçlar")]
        public string SpecialNeeds { get; set; } = string.Empty;

        [Display(Name = "Eğitim Durumu")]
        public string EducationStatus { get; set; } = string.Empty;

        [Display(Name = "Ek Notlar")]
        public string AdditionalNotes { get; set; } = string.Empty;
    }

    public class ContactInformation
    {
        [Required(ErrorMessage = "Adres gereklidir")]
        [Display(Name = "Adres")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şehir gereklidir")]
        [Display(Name = "Şehir")]
        public string City { get; set; } = string.Empty;

        [Display(Name = "Acil Durum İletişim Kişisi")]
        public string EmergencyContact { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [Display(Name = "Acil Durum Telefonu")]
        public string EmergencyPhone { get; set; } = string.Empty;
    }

    public class NotificationSettings
    {
        [Display(Name = "E-posta Bildirimleri")]
        public bool EmailNotifications { get; set; } = true;

        [Display(Name = "SMS Bildirimleri")]
        public bool SmsNotifications { get; set; } = true;

        [Display(Name = "İlerleme Raporu Bildirimleri")]
        public bool ProgressReports { get; set; } = true;

        [Display(Name = "Yeni Eğitim Bildirimleri")]
        public bool NewCourseNotifications { get; set; } = true;
    }
} 