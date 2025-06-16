using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutismEducationPlatform.Web.Models
{
    public class TaleProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaleId { get; set; }

        [Required]
        public string UserId { get; set; }

        public int ProgressPercentage { get; set; }

        public DateTime LastInteraction { get; set; }

        [ForeignKey("TaleId")]
        public virtual Tale Tale { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
} 