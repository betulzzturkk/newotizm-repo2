using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models
{
    public class TaleProgressModel
    {
        [Required]
        public int TaleId { get; set; }

        [Required]
        public int Progress { get; set; }
    }
} 