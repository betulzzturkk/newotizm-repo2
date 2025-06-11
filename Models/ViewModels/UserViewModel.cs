using System;
using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Soyad")]
        public string Surname { get; set; } = string.Empty;

        [Display(Name = "E-posta")]
        public string? Email { get; set; }

        [Display(Name = "Rol")]
        public string? Role { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Ad Soyad")]
        public string FullName => $"{Name} {Surname}";
    }
} 