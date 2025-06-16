using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class MannerProgress
    {
        public int Id { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string MannerName { get; set; }
        public int Progress { get; set; }
        public int InteractionCount { get; set; }
        public DateTime LastInteraction { get; set; }
    }
} 