using System;
using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Models
{
    public class TrafficSign
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string SoundPath { get; set; }
    }
} 