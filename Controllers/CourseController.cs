using Microsoft.AspNetCore.Mvc;
using AutismEducationPlatform.Web.Data;
using AutismEducationPlatform.Web.Models;
using AutismEducationPlatform.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace AutismEducationPlatform.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<bool> IsInstructor()
        {
            if (!User.Identity.IsAuthenticated)
                return false;

            var user = await _userManager.GetUserAsync(User);
            return await _userManager.IsInRoleAsync(user, "Instructor");
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.IsInstructor = await IsInstructor();
            return View();
        }

        public IActionResult Animals()
        {
            var animals = new List<AnimalViewModel>
            {
                new AnimalViewModel
                {
                    Id = 1,
                    Name = "Kedi",
                    Description = "Miyav!",
                    ImagePath = "/images/animals/cat.jpg",
                    SoundPath = "/sounds/animals/kedi.mp3"
                },
                new AnimalViewModel
                {
                    Id = 2,
                    Name = "Köpek",
                    Description = "Hav hav!",
                    ImagePath = "/images/animals/dog.jpg",
                    SoundPath = "/sounds/animals/kopek.mp3"
                },
                new AnimalViewModel
                {
                    Id = 3,
                    Name = "Kuş",
                    Description = "Cik cik!",
                    ImagePath = "/images/animals/bird.jpg",
                    SoundPath = "/sounds/animals/kus.mp3"
                },
                new AnimalViewModel
                {
                    Id = 4,
                    Name = "Balık",
                    Description = "Glug glug!",
                    ImagePath = "/images/animals/fish.jpg",
                    SoundPath = "/sounds/animals/balik.mp3"
                },
                new AnimalViewModel
                {
                    Id = 5,
                    Name = "Tavşan",
                    Description = "Hıh hıh!",
                    ImagePath = "/images/animals/rabbit.jpg",
                    SoundPath = "/sounds/animals/tavsan.mp3"
                },
                new AnimalViewModel
                {
                    Id = 6,
                    Name = "Kurbağa",
                    Description = "Vrak vrak!",
                    ImagePath = "/images/animals/frog.jpg",
                    SoundPath = "/sounds/animals/kurbaga.mp3"
                },
                new AnimalViewModel
                {
                    Id = 7,
                    Name = "Aslan",
                    Description = "Kükreeme!",
                    ImagePath = "/images/animals/lion.jpg",
                    SoundPath = "/sounds/animals/aslan.mp3"
                },
                new AnimalViewModel
                {
                    Id = 8,
                    Name = "Tavuk",
                    Description = "Gıt gıt gıdak!",
                    ImagePath = "/images/animals/chicken.jpg",
                    SoundPath = "/sounds/animals/tavuk.mp3"
                }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.AnimalProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.AnimalId, p => p.Progress);

                foreach (var animal in animals)
                {
                    if (progresses.ContainsKey(animal.Id))
                    {
                        animal.Progress = progresses[animal.Id];
                    }
                }
            }

            return View(animals);
        }

        public IActionResult Colors()
        {
            var colors = new List<ColorViewModel>
            {
                new ColorViewModel { Name = "Kırmızı", Description = "Canlı ve dikkat çekici bir renktir.", ImagePath = "/images/colors/kirmizi.jpg", HexCode = "#FF0000", Example = "Kırmızı elma", SoundPath = "/sounds/colors/kirmizi.mp3" },
                new ColorViewModel { Name = "Mavi", Description = "Gökyüzü ve denizin rengidir.", ImagePath = "/images/colors/mavi.jpg", HexCode = "#0000FF", Example = "Mavi gökyüzü", SoundPath = "/sounds/colors/mavi.mp3" },
                new ColorViewModel { Name = "Sarı", Description = "Güneşin rengidir.", ImagePath = "/images/colors/sari.jpg", HexCode = "#FFFF00", Example = "Sarı limon", SoundPath = "/sounds/colors/sari.mp3" },
                new ColorViewModel { Name = "Yeşil", Description = "Doğanın ve çimenlerin rengidir.", ImagePath = "/images/colors/yesil.jpg", HexCode = "#00FF00", Example = "Yeşil yaprak", SoundPath = "/sounds/colors/yesil.mp3" },
                new ColorViewModel { Name = "Turuncu", Description = "Portakalın rengidir.", ImagePath = "/images/colors/turuncu.jpg", HexCode = "#FFA500", Example = "Turuncu portakal", SoundPath = "/sounds/colors/turuncu.mp3" },
                new ColorViewModel { Name = "Mor", Description = "Çiçeklerin güzel rengidir.", ImagePath = "/images/colors/mor.jpg", HexCode = "#800080", Example = "Mor menekşe", SoundPath = "/sounds/colors/mor.mp3" },
                new ColorViewModel { Name = "Pembe", Description = "Tatlı ve yumuşak bir renktir.", ImagePath = "/images/colors/pembe.jpg", HexCode = "#FFC0CB", Example = "Pembe şeker", SoundPath = "/sounds/colors/pembe.mp3" },
                new ColorViewModel { Name = "Kahverengi", Description = "Toprağın rengidir.", ImagePath = "/images/colors/kahverengi.jpg", HexCode = "#8B4513", Example = "Kahverengi ağaç", SoundPath = "/sounds/colors/kahverengi.mp3" },
                new ColorViewModel { Name = "Siyah", Description = "Geceyi temsil eder.", ImagePath = "/images/colors/siyah.jpg", HexCode = "#000000", Example = "Siyah kedi", SoundPath = "/sounds/colors/siyah.mp3" },
                new ColorViewModel { Name = "Beyaz", Description = "Saflığı ve temizliği simgeler.", ImagePath = "/images/colors/beyaz.jpg", HexCode = "#FFFFFF", Example = "Beyaz bulut", SoundPath = "/sounds/colors/beyaz.mp3" }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.ColorProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.ColorName, p => p.Progress);

                foreach (var color in colors)
                {
                    if (progresses.ContainsKey(color.Name))
                    {
                        color.Progress = progresses[color.Name];
                    }
                }
            }

            return View(colors);
        }

        public IActionResult Shapes()
        {
            var shapes = new List<ShapeViewModel>
            {
                new ShapeViewModel { Name = "Daire", Description = "Yuvarlak şekil", ImagePath = "/images/shapes/circle.jpg", SoundPath = "/sounds/shapes/daire.mp3" },
                new ShapeViewModel { Name = "Kare", Description = "Dört kenarlı eşit şekil", ImagePath = "/images/shapes/square.jpg", SoundPath = "/sounds/shapes/kare.mp3" },
                new ShapeViewModel { Name = "Üçgen", Description = "Üç kenarlı şekil", ImagePath = "/images/shapes/triangle.jpg", SoundPath = "/sounds/shapes/ucgen.mp3" },
                new ShapeViewModel { Name = "Dikdörtgen", Description = "Dört kenarlı şekil", ImagePath = "/images/shapes/rectangle.jpg", SoundPath = "/sounds/shapes/dikdortgen.mp3" },
                new ShapeViewModel { Name = "Yıldız", Description = "Beş köşeli şekil", ImagePath = "/images/shapes/star.jpg", SoundPath = "/sounds/shapes/yildiz.mp3" },
                new ShapeViewModel { Name = "Kalp", Description = "Sevgi şekli", ImagePath = "/images/shapes/heart.jpg", SoundPath = "/sounds/shapes/kalp.mp3" }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.ShapeProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.ShapeName, p => p.Progress);

                foreach (var shape in shapes)
                {
                    if (progresses.ContainsKey(shape.Name))
                    {
                        shape.Progress = progresses[shape.Name];
                    }
                }
            }

            return View(shapes);
        }

        public IActionResult Numbers()
        {
            var numbers = new List<NumberViewModel>
            {
                new NumberViewModel { Value = 1, Description = "Bir", ImagePath = "/images/numbers/1.jpg", SoundPath = "/sounds/numbers/1.mp3" },
                new NumberViewModel { Value = 2, Description = "İki", ImagePath = "/images/numbers/2.jpg", SoundPath = "/sounds/numbers/2.mp3" },
                new NumberViewModel { Value = 3, Description = "Üç", ImagePath = "/images/numbers/3.jpg", SoundPath = "/sounds/numbers/3.mp3" },
                new NumberViewModel { Value = 4, Description = "Dört", ImagePath = "/images/numbers/4.jpg", SoundPath = "/sounds/numbers/4.mp3" },
                new NumberViewModel { Value = 5, Description = "Beş", ImagePath = "/images/numbers/5.jpg", SoundPath = "/sounds/numbers/5.mp3" },
                new NumberViewModel { Value = 6, Description = "Altı", ImagePath = "/images/numbers/6.jpg", SoundPath = "/sounds/numbers/6.mp3" },
                new NumberViewModel { Value = 7, Description = "Yedi", ImagePath = "/images/numbers/7.jpg", SoundPath = "/sounds/numbers/7.mp3" },
                new NumberViewModel { Value = 8, Description = "Sekiz", ImagePath = "/images/numbers/8.jpg", SoundPath = "/sounds/numbers/8.mp3" },
                new NumberViewModel { Value = 9, Description = "Dokuz", ImagePath = "/images/numbers/9.jpg", SoundPath = "/sounds/numbers/9.mp3" },
                new NumberViewModel { Value = 10, Description = "On", ImagePath = "/images/numbers/10.jpg", SoundPath = "/sounds/numbers/10.mp3" }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.NumberProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.NumberValue, p => p.Progress);

                foreach (var number in numbers)
                {
                    if (progresses.ContainsKey(number.Value))
                    {
                        number.Progress = progresses[number.Value];
                    }
                }
            }

            return View(numbers);
        }

        public IActionResult Tales()
        {
            var tales = new List<TaleViewModel>
            {
                new TaleViewModel { 
                    Id = 1,
                    Title = "Kırmızı Başlıklı Kız", 
                    Description = "Orman macerası", 
                    ImagePath = "/images/tales/kirmizi-baslikli-kiz.jpg", 
                    SoundPath = "/sounds/tales/kirmizi-baslikli-kiz.mp3" 
                },
                new TaleViewModel { 
                    Id = 2,
                    Title = "Uyuyan Güzel", 
                    Description = "Prenses masalı", 
                    ImagePath = "/images/tales/uyuyan-guzel.jpg", 
                    SoundPath = "/sounds/tales/uyuyan-guzel.mp3" 
                },
                new TaleViewModel { 
                    Id = 3,
                    Title = "Küçük Prens", 
                    Description = "Gezegenler arası yolculuk", 
                    ImagePath = "/images/tales/kucuk-prens.jpg", 
                    SoundPath = "/sounds/tales/kucuk-prens.mp3" 
                }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.TaleProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.TaleId, p => p.ProgressPercentage);

                foreach (var tale in tales)
                {
                    if (progresses.ContainsKey(tale.Id))
                    {
                        tale.Progress = (int)progresses[tale.Id];
                        tale.ProgressPercentage = progresses[tale.Id];
                    }
                }
            }

            return View(tales);
        }

        public IActionResult TrafficSigns()
        {
            var signs = new List<TrafficSignViewModel>
            {
                new TrafficSignViewModel { Name = "Dur", ImagePath = "/images/traffic/dur.jpg", SoundPath = "/sounds/traffic/dur.mp3", Description = "Dur işareti: Yolun kesiştiği noktada durulması gerektiğini belirtir." },
                new TrafficSignViewModel { Name = "Yaya Geçidi", ImagePath = "/images/traffic/yaya.jpg", SoundPath = "/sounds/traffic/yaya.mp3", Description = "Yaya geçidi işareti: Yayaların karşıdan karşıya geçebileceği yeri gösterir." },
                new TrafficSignViewModel { Name = "Okul Geçidi", ImagePath = "/images/traffic/okul.jpg", SoundPath = "/sounds/traffic/okul.mp3", Description = "Okul geçidi işareti: Okul yakınında olduğunu ve dikkatli olunması gerektiğini belirtir." },
                new TrafficSignViewModel { Name = "Işıklı İşaret", ImagePath = "/images/traffic/isikli.jpg", SoundPath = "/sounds/traffic/isikli.mp3", Description = "Işıklı işaret: Trafik ışıklarının olduğu yeri gösterir." },
                new TrafficSignViewModel { Name = "Yön", ImagePath = "/images/traffic/yon.jpg", SoundPath = "/sounds/traffic/yon.mp3", Description = "Yön işareti: Gidilebilecek yönü gösterir." }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.TrafficSignProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.SignName, p => p.Progress);

                foreach (var sign in signs)
                {
                    if (progresses.ContainsKey(sign.Name))
                    {
                        sign.Progress = progresses[sign.Name];
                    }
                }
            }

            return View(signs);
        }

        public IActionResult Manners()
        {
            var manners = new List<MannersViewModel>
            {
                new MannersViewModel
                {
                    Title = "Teşekkür Etmek",
                    Description = "Teşekkür etmek, başkalarına karşı saygı göstermenin bir yoludur.",
                    ImagePath = "/images/tesekkur.jpg",
                    Category = "Sosyal",
                    Color = "bg-success",
                    Example = "Birisi size bir şey verdiğinde",
                    CorrectBehavior = "Teşekkür ederim demek"
                }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.MannerProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.MannerName, p => p.Progress);

                foreach (var manner in manners)
                {
                    if (progresses.ContainsKey(manner.Title))
                    {
                        manner.Progress = progresses[manner.Title];
                    }
                }
            }

            return View(manners);
        }

        public IActionResult SelfExpression()
        {
            var expressions = new List<MannersViewModel>
            {
                new MannersViewModel
                {
                    Title = "Teşekkür Etmek",
                    Description = "Birine yardım ettiğinde veya bir şey verdiğinde teşekkür etmeyi öğrenelim.",
                    ImagePath = "/images/manners/tesekkur.jpg",
                    Category = "Görgü Kuralları",
                    Color = "bg-success",
                    Example = "Teşekkür ederim.",
                    CorrectBehavior = "Teşekkür ederken göz teması kur ve gülümse.",
                    SoundPath = "/sounds/manners/tesekkuretmek.mp3"
                },
                new MannersViewModel
                {
                    Title = "Özür Dilemek",
                    Description = "Bir hata yaptığında özür dilemeyi öğrenelim.",
                    ImagePath = "/images/manners/ozur.jpg",
                    Category = "Görgü Kuralları",
                    Color = "bg-info",
                    Example = "Özür dilerim.",
                    CorrectBehavior = "Özür dilerken göz teması kur ve ciddi ol.",
                    SoundPath = "/sounds/manners/ozurdilemek.mp3"
                },
                new MannersViewModel
                {
                    Title = "Selamlaşmak",
                    Description = "İnsanlarla selamlaşmayı öğrenelim.",
                    ImagePath = "/images/manners/selam.jpg",
                    Category = "Görgü Kuralları",
                    Color = "bg-warning",
                    Example = "Merhaba, nasılsın?",
                    CorrectBehavior = "Selamlaşırken göz teması kur ve gülümse.",
                    SoundPath = "/sounds/manners/selamlasmak.mp3"
                },
                new MannersViewModel
                {
                    Title = "Dinlemek",
                    Description = "Başkalarını dinlemeyi öğrenelim.",
                    ImagePath = "/images/manners/dinle.jpg",
                    Category = "Görgü Kuralları",
                    Color = "bg-primary",
                    Example = "Seni dinliyorum.",
                    CorrectBehavior = "Dinlerken göz teması kur ve sözünü kesme.",
                    SoundPath = "/sounds/manners/dinlemek.mp3"
                },
                new MannersViewModel
                {
                    Title = "Kapı Çalmak",
                    Description = "Bir odaya girmeden önce kapıyı çalmayı öğrenelim.",
                    ImagePath = "/images/manners/kapi.jpg",
                    Category = "Görgü Kuralları",
                    Color = "bg-danger",
                    Example = "Girebilir miyim?",
                    CorrectBehavior = "Kapıyı çal ve cevap bekle.",
                    SoundPath = "/sounds/manners/kapiyicalmak.mp3"
                },
                new MannersViewModel
                {
                    Title = "Paylaşmak",
                    Description = "Eşyalarını başkalarıyla paylaşmayı öğrenelim.",
                    ImagePath = "/images/manners/paylasma.jpg",
                    Category = "Görgü Kuralları",
                    Color = "bg-secondary",
                    Example = "Bunu seninle paylaşmak istiyorum.",
                    CorrectBehavior = "Paylaşırken nazik ol ve karşılıklı saygı göster.",
                    SoundPath = "/sounds/manners/paylasmak.mp3"
                }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.MannerProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.MannerName, p => p.Progress);

                foreach (var expression in expressions)
                {
                    var key = expression.Title.ToLower().Replace(" ", "").Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("ö", "o").Replace("ç", "c");
                    if (progresses.ContainsKey(key))
                    {
                        expression.Progress = progresses[key];
                    }
                }
            }

            ViewBag.IsInstructor = IsInstructor();
            return View(expressions);
        }

        public IActionResult EducationalVideos() => View();

        public IActionResult Details(int id)
        {
            switch (id)
            {
                case 1:
                    return RedirectToAction("Animals");
                case 2:
                    return RedirectToAction("Colors");
                case 3:
                    return RedirectToAction("Numbers");
                case 4:
                    return RedirectToAction("Shapes");
                case 5:
                    return RedirectToAction("Tales");
                case 6:
                    return RedirectToAction("TrafficSigns");
                case 7:
                    return RedirectToAction("Manners");
                case 8:
                    return RedirectToAction("EducationalVideos");
                case 9:
                    return RedirectToAction("SelfExpression");
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAnimalProgress(int animalId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Lütfen giriş yapın.", requireLogin = true });
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progress = await _context.AnimalProgress
                    .FirstOrDefaultAsync(p => p.AnimalId == animalId && p.UserId == userId);

                if (progress == null)
                {
                    progress = new AnimalProgress
                    {
                        AnimalId = animalId,
                        UserId = userId,
                        Progress = 0,
                        InteractionCount = 0,
                        LastInteraction = DateTime.UtcNow
                    };
                    _context.AnimalProgress.Add(progress);
                }

                progress.InteractionCount++;
                progress.LastInteraction = DateTime.UtcNow;
                
                // Her tıklamada %20 artır, maksimum %100
                progress.Progress = Math.Min(100, progress.Progress + 20);

                await _context.SaveChangesAsync();

                return Json(new { success = true, progress = progress.Progress });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveColorProgress([FromBody] ColorProgressInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Lütfen giriş yapın.", requireLogin = true });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var progress = await _context.ColorProgress.FirstOrDefaultAsync(p => p.ColorName == model.ColorName && p.UserId == userId);

            if (progress == null)
            {
                progress = new ColorProgress
                {
                    ColorName = model.ColorName,
                    UserId = userId,
                    Progress = model.Progress,
                    InteractionCount = 1,
                    LastInteraction = DateTime.UtcNow
                };
                _context.ColorProgress.Add(progress);
            }
            else
            {
                progress.Progress = model.Progress;
                progress.InteractionCount++;
                progress.LastInteraction = DateTime.UtcNow;
                _context.ColorProgress.Update(progress);
            }

            // CourseProgress tablosunu güncelle
            var courseProgress = await _context.CourseProgress
                .FirstOrDefaultAsync(cp => cp.UserId == userId && cp.CourseName == "Renkler");

            if (courseProgress == null)
            {
                courseProgress = new CourseProgress
                {
                    UserId = userId,
                    CourseId = 2, // Renkler kursunun ID'si
                    CourseName = "Renkler",
                    ProgressPercentage = model.Progress,
                    LastInteraction = DateTime.UtcNow,
                    CompletedActivities = model.Progress >= 100 ? 1 : 0,
                    TotalActivities = 1
                };
                _context.CourseProgress.Add(courseProgress);
            }
            else
            {
                courseProgress.ProgressPercentage = model.Progress;
                courseProgress.LastInteraction = DateTime.UtcNow;
                courseProgress.CompletedActivities = model.Progress >= 100 ? 1 : 0;
                _context.CourseProgress.Update(courseProgress);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, progress = progress.Progress });
        }

        [HttpPost]
        public async Task<IActionResult> SaveNumberProgress([FromBody] NumberProgressInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Lütfen giriş yapın.", requireLogin = true });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var progress = await _context.NumberProgress.FirstOrDefaultAsync(p => p.NumberValue == model.NumberValue && p.UserId == userId);

            if (progress == null)
            {
                progress = new NumberProgress
                {
                    NumberValue = model.NumberValue,
                    UserId = userId,
                    Progress = model.Progress,
                    InteractionCount = 1,
                    LastInteraction = DateTime.UtcNow
                };
                _context.NumberProgress.Add(progress);
            }
            else
            {
                progress.Progress = model.Progress;
                progress.InteractionCount++;
                progress.LastInteraction = DateTime.UtcNow;
                _context.NumberProgress.Update(progress);
            }

            // CourseProgress tablosunu güncelle
            var courseProgress = await _context.CourseProgress
                .FirstOrDefaultAsync(cp => cp.UserId == userId && cp.CourseName == "Sayılar");

            if (courseProgress == null)
            {
                courseProgress = new CourseProgress
                {
                    UserId = userId,
                    CourseId = 3, // Sayılar kursunun ID'si
                    CourseName = "Sayılar",
                    ProgressPercentage = model.Progress,
                    LastInteraction = DateTime.UtcNow,
                    CompletedActivities = model.Progress >= 100 ? 1 : 0,
                    TotalActivities = 1
                };
                _context.CourseProgress.Add(courseProgress);
            }
            else
            {
                courseProgress.ProgressPercentage = model.Progress;
                courseProgress.LastInteraction = DateTime.UtcNow;
                courseProgress.CompletedActivities = model.Progress >= 100 ? 1 : 0;
                _context.CourseProgress.Update(courseProgress);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, progress = progress.Progress });
        }

        [HttpPost]
        public async Task<IActionResult> SaveTrafficSignProgress([FromBody] TrafficSignProgressInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Lütfen giriş yapın.", requireLogin = true });
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var progress = await _context.TrafficSignProgress.FirstOrDefaultAsync(p => p.SignName == model.SignName && p.UserId == userId);
            if (progress == null)
            {
                progress = new TrafficSignProgress
                {
                    SignName = model.SignName,
                    UserId = userId,
                    Progress = model.Progress,
                    InteractionCount = 1,
                    LastInteraction = DateTime.UtcNow
                };
                _context.TrafficSignProgress.Add(progress);
            }
            else
            {
                progress.Progress = model.Progress;
                progress.InteractionCount++;
                progress.LastInteraction = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            return Json(new { success = true, progress = progress.Progress });
        }

        [HttpPost]
        public async Task<IActionResult> SaveMannerProgress([FromBody] MannerProgressInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Lütfen giriş yapın.", requireLogin = true });
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progress = await _context.MannerProgress
                    .FirstOrDefaultAsync(p => p.MannerName == model.MannerName && p.UserId == userId);

                if (progress == null)
                {
                    progress = new MannerProgress
                    {
                        MannerName = model.MannerName,
                        UserId = userId,
                        Progress = model.Progress,
                        InteractionCount = 1,
                        LastInteraction = DateTime.UtcNow
                    };
                    _context.MannerProgress.Add(progress);
                }
                else
                {
                    progress.Progress = model.Progress;
                    progress.InteractionCount++;
                    progress.LastInteraction = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true, progress = progress.Progress });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public class ColorProgressInputModel
        {
            public string ColorName { get; set; }
            public int Progress { get; set; }
        }

        public class NumberProgressInputModel
        {
            public int NumberValue { get; set; }
            public int Progress { get; set; }
        }

        public class TrafficSignProgressInputModel
        {
            public string SignName { get; set; }
            public int Progress { get; set; }
        }

        public class MannerProgressInputModel
        {
            public string MannerName { get; set; }
            public int Progress { get; set; }
        }

        public IActionResult Exams()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var examResults = _context.ExamResults
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CompletedAt)
                .ToList();

            ViewBag.ExamResults = examResults;
            return View();
        }

        public IActionResult Exam(int level)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var examResult = _context.ExamResults
                .FirstOrDefault(r => r.UserId == userId && r.ExamLevel == level.ToString());

            if (examResult != null)
            {
                return RedirectToAction("Exams");
            }

            var questions = GetRandomQuestions();

            ViewBag.Questions = questions;
            ViewBag.TotalQuestions = questions.Count;
            ViewBag.Level = level.ToString();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveExamResult([FromBody] AutismEducationPlatform.Web.Models.ViewModels.ExamResultViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var examResult = _context.ExamResults
                .FirstOrDefault(r => r.UserId == userId && r.ExamLevel == model.ExamLevel);

            if (examResult != null)
            {
                return RedirectToAction("Exams");
            }

            var result = new ExamResult
            {
                UserId = userId,
                ExamLevel = model.ExamLevel,
                ExamName = $"Sınav {model.ExamLevel}",
                CorrectCount = model.CorrectCount,
                WrongCount = model.WrongCount,
                TotalQuestions = model.TotalQuestions,
                Score = model.Score,
                ExamDate = DateTime.UtcNow
            };

            _context.ExamResults.Add(result);
            await _context.SaveChangesAsync();

            if (model.Answers != null)
            {
                foreach (var ans in model.Answers)
                {
                    var answer = new ExamAnswer
                    {
                        ExamResultId = result.Id,
                        QuestionText = ans.QuestionText,
                        SelectedOption = ans.SelectedOption,
                        CorrectOption = ans.CorrectOption,
                        IsCorrect = ans.IsCorrect
                    };
                    _context.ExamAnswers.Add(answer);
                }
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        public class ExamResultInputModel
        {
            [Required]
            public int ExamLevel { get; set; }
            [Required]
            public int CorrectCount { get; set; }
            [Required]
            public int WrongCount { get; set; }
            [Required]
            public int TotalQuestions { get; set; }
            [Required]
            public double Score { get; set; }
            public List<ExamAnswerInputModel> Answers { get; set; }
        }

        private List<object> GetRandomQuestions()
        {
            var allQuestions = new List<object>
            {
                // Hayvanlar
                new { content = "Aşağıdakilerden hangisi bir hayvan değildir?", options = new[] { "Kedi", "Araba", "Köpek", "Kuş" }, correctAnswer = 1 },
                new { content = "Hangi hayvan uçabilir?", options = new[] { "Kedi", "Köpek", "Kuş", "Balık" }, correctAnswer = 2 },
                new { content = "Hangi hayvan suda yaşar?", options = new[] { "Kedi", "Köpek", "Kuş", "Balık" }, correctAnswer = 3 },
                
                // Renkler
                new { content = "Hangi renk gökyüzünün rengidir?", options = new[] { "Kırmızı", "Mavi", "Yeşil", "Sarı" }, correctAnswer = 1 },
                new { content = "Hangi renk güneşin rengidir?", options = new[] { "Kırmızı", "Mavi", "Yeşil", "Sarı" }, correctAnswer = 3 },
                new { content = "Hangi renk çimenin rengidir?", options = new[] { "Kırmızı", "Mavi", "Yeşil", "Sarı" }, correctAnswer = 2 },
                
                // Sayılar
                new { content = "2 + 3 = ?", options = new[] { "4", "5", "6", "7" }, correctAnswer = 1 },
                new { content = "5 - 2 = ?", options = new[] { "2", "3", "4", "5" }, correctAnswer = 1 },
                new { content = "4 x 2 = ?", options = new[] { "6", "7", "8", "9" }, correctAnswer = 2 },
                
                // Şekiller
                new { content = "Aşağıdakilerden hangisi bir geometrik şekil değildir?", options = new[] { "Kare", "Üçgen", "Kalem", "Daire" }, correctAnswer = 2 },
                new { content = "Hangi şeklin 4 kenarı vardır?", options = new[] { "Üçgen", "Kare", "Daire", "Yıldız" }, correctAnswer = 1 },
                new { content = "Hangi şeklin 3 kenarı vardır?", options = new[] { "Kare", "Üçgen", "Daire", "Yıldız" }, correctAnswer = 1 },
                
                // Trafik İşaretleri
                new { content = "Aşağıdakilerden hangisi bir trafik işareti değildir?", options = new[] { "Dur", "Yol Ver", "Okul", "Ağaç" }, correctAnswer = 3 },
                new { content = "Hangi işaret yaya geçidini gösterir?", options = new[] { "Dur", "Yol Ver", "Yaya Geçidi", "Okul" }, correctAnswer = 2 },
                new { content = "Hangi işaret okul bölgesini gösterir?", options = new[] { "Dur", "Yol Ver", "Yaya Geçidi", "Okul" }, correctAnswer = 3 },
                
                // Görgü Kuralları
                new { content = "Birisi konuşurken ne yapmalıyız?", options = new[] { "Sözünü kesmek", "Dinlemek", "Başka şeylerle ilgilenmek", "Konuşmak" }, correctAnswer = 1 },
                new { content = "Yemek yerken ne yapmalıyız?", options = new[] { "Konuşmak", "Ağzımız açık çiğnemek", "Ağzımız kapalı çiğnemek", "Hızlı yemek" }, correctAnswer = 2 },
                new { content = "Birisi bize yardım ettiğinde ne demeliyiz?", options = new[] { "Hiçbir şey", "Teşekkür ederim", "Hoşça kal", "Merhaba" }, correctAnswer = 1 }
            };

            var random = new Random();
            return allQuestions.OrderBy(x => random.Next()).Take(5).ToList();
        }

        [HttpPost]
        public IActionResult SubmitExam([FromBody] ExamResultViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Kullanıcı girişi yapılmamış." });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            var examResult = new ExamResult
            {
                UserId = userId,
                ExamLevel = model.ExamLevel,
                ExamName = $"Sınav {model.ExamLevel}",
                CorrectCount = model.CorrectCount,
                WrongCount = model.WrongCount,
                TotalQuestions = model.TotalQuestions,
                Score = model.Score,
                ExamDate = DateTime.UtcNow
            };

            _context.ExamResults.Add(examResult);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}