using Microsoft.AspNetCore.Mvc;
using AutismEducationPlatform.Web.Data;
using AutismEducationPlatform.Web.Models;
using AutismEducationPlatform.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = new List<Course>
            {
                new Course { Id = 1, Name = "HAYVANLAR", Description = "Hayvanları tanıyalım", ImageUrl = "/images/courses/animals.jpg", Category = "Temel Eğitim" },
                new Course { Id = 2, Name = "RENKLER", Description = "Renkleri öğrenelim", ImageUrl = "/images/courses/colors.jpg", Category = "Temel Eğitim" },
                new Course { Id = 3, Name = "ŞEKİLLER", Description = "Şekilleri keşfedelim", ImageUrl = "/images/courses/shapes.jpg", Category = "Temel Eğitim" },
                new Course { Id = 4, Name = "SAYILAR", Description = "Sayıları öğrenelim", ImageUrl = "/images/courses/numbers.jpg", Category = "Matematik" },
                new Course { Id = 5, Name = "HİKAYELER", Description = "Hikayeler dinleyelim", ImageUrl = "/images/courses/tales.jpg", Category = "Dil Gelişimi" },
                new Course { Id = 6, Name = "TRAFİK İŞARETLERİ", Description = "Trafik işaretlerini öğrenelim", ImageUrl = "/images/courses/traffic.jpg", Category = "Güvenlik" },
                new Course { Id = 7, Name = "GÖRGÜ KURALLARI", Description = "Görgü kurallarını öğrenelim", ImageUrl = "/images/courses/manners.jpg", Category = "Sosyal Beceriler" },
                new Course { Id = 8, Name = "EĞİTİM VİDEOLARI", Description = "Eğitici videolar izleyelim", ImageUrl = "/images/courses/videos.jpg", Category = "Multimedya" }
            };

            return View(courses);
        }

        public IActionResult Animals()
        {
            var animals = new List<AnimalViewModel>
            {
                new AnimalViewModel 
                { 
                    Name = "Kedi",
                    Description = "Evcil bir hayvandır. Miyav sesi çıkarır.",
                    ImagePath = "/images/animals/cat.jpg",
                    SoundPath = "/sounds/animals/cat.mp3"
                },
                new AnimalViewModel 
                { 
                    Name = "Köpek",
                    Description = "Evcil bir hayvandır. Hav hav sesi çıkarır.",
                    ImagePath = "/images/animals/dog.jpg",
                    SoundPath = "/sounds/animals/dog.mp3"
                },
                new AnimalViewModel 
                { 
                    Name = "İnek",
                    Description = "Çiftlik hayvanıdır. Möö sesi çıkarır.",
                    ImagePath = "/images/animals/cow.jpg",
                    SoundPath = "/sounds/animals/cow.mp3"
                },
                new AnimalViewModel 
                { 
                    Name = "Kuş",
                    Description = "Uçabilen bir hayvandır. Cik cik sesi çıkarır.",
                    ImagePath = "/images/animals/bird.jpg",
                    SoundPath = "/sounds/animals/bird.mp3"
                },
                new AnimalViewModel 
                { 
                    Name = "At",
                    Description = "Çiftlik hayvanıdır. Kişneme sesi çıkarır.",
                    ImagePath = "/images/animals/horse.jpg",
                    SoundPath = "/sounds/animals/horse.mp3"
                },
                new AnimalViewModel 
                { 
                    Name = "Tavuk",
                    Description = "Çiftlik hayvanıdır. Gıt gıt sesi çıkarır.",
                    ImagePath = "/images/animals/chicken.jpg",
                    SoundPath = "/sounds/animals/chicken.mp3"
                },
                new AnimalViewModel 
                { 
                    Name = "Koyun",
                    Description = "Çiftlik hayvanıdır. Mee sesi çıkarır.",
                    ImagePath = "/images/animals/sheep.jpg",
                    SoundPath = "/sounds/animals/sheep.mp3"
                },
                new AnimalViewModel 
                { 
                    Name = "Aslan",
                    Description = "Vahşi bir hayvandır. Kükreme sesi çıkarır.",
                    ImagePath = "/images/animals/lion.jpg",
                    SoundPath = "/sounds/animals/lion.mp3"
                }
            };

            return View(animals);
        }

        public IActionResult Colors() => View();
        public IActionResult Shapes() => View();
        public IActionResult Numbers() => View();
        public IActionResult Tales() => View();
        public IActionResult TrafficSigns() => View();
        public IActionResult Manners() => View();
        public IActionResult EducationalVideos() => View();

        public IActionResult Details(int id)
        {
            var viewName = id switch
            {
                1 => "Animals",
                2 => "Colors",
                3 => "Shapes",
                4 => "Numbers",
                5 => "Tales",
                6 => "TrafficSigns",
                7 => "Manners",
                8 => "EducationalVideos",
                _ => "Index"
            };

            return View(viewName);
        }
    }
}