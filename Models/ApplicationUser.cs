using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(100)]
        public string? Surname { get; set; }

        [StringLength(50)]
        public string? Role { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // Child Information
        [StringLength(100)]
        [PersonalData]
        public string? ChildName { get; set; }

        [Range(1, 18)]
        [PersonalData]
        public int? ChildAge { get; set; }

        [StringLength(200)]
        [PersonalData]
        public string? ChildDiagnosis { get; set; }

        // Contact Information
        [StringLength(500)]
        [PersonalData]
        public string? Address { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}".Trim();

        public virtual ICollection<IdentityUserToken<string>> UserTokens { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }

        // Veli için özel alanlar
        public string? Specialization { get; set; }
        public int? Experience { get; set; }

        public ApplicationUser()
        {
            UserTokens = new HashSet<IdentityUserToken<string>>();
        }
    }
} 