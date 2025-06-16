using System;
using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models
{
    public class Tale
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 