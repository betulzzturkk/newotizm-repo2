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
                    Name = "İnek",
                    Description = "Möö!",
                    ImagePath = "/images/animals/cow.jpg",
                    SoundPath = "/sounds/animals/inek.mp3"
                },
                new AnimalViewModel
                {
                    Id = 4,
                    Name = "Kuş",
                    Description = "Cik cik!",
                    ImagePath = "/images/animals/bird.jpg",
                    SoundPath = "/sounds/animals/kus.mp3"
                },
                new AnimalViewModel
                {
                    Id = 5,
                    Name = "At",
                    Description = "Nayy!",
                    ImagePath = "/images/animals/horse.jpg",
                    SoundPath = "/sounds/animals/at.mp3"
                },
                new AnimalViewModel
                {
                    Id = 6,
                    Name = "Fil",
                    Description = "Puuuuh!",
                    ImagePath = "/images/animals/elephant.jpg",
                    SoundPath = "/sounds/animals/fil.mp3"
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

            // Kullanıcı giriş yapmışsa, ilerleme bilgilerini getir
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.AnimalProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.AnimalId, p => p.Progress);

                // Her hayvan için ilerleme bilgisini ekle
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

            // Kullanıcı giriş yapmışsa, ilerleme bilgilerini getir
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.AnimalProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.AnimalId, p => p.Progress); // NOT: Renkler için ayrı bir tablo/model önerilir!
                // Şimdilik örnek olarak bırakıldı.
            }

            return View(colors);
        }

        public IActionResult Shapes()
        {
            var shapes = new List<ShapeViewModel>
            {
                new ShapeViewModel { Name = "Kare", ImagePath = "/images/shapes/kare.jpg", SoundPath = "/sounds/shapes/kare.mp3", Description = "Dört kenarı eşit olan şekil." },
                new ShapeViewModel { Name = "Dikdörtgen", ImagePath = "/images/shapes/dikdortgen.jpg", SoundPath = "/sounds/shapes/dikdortgen.mp3", Description = "Karşılıklı kenarları eşit olan dörtgen." },
                new ShapeViewModel { Name = "Daire", ImagePath = "/images/shapes/daire.jpg", SoundPath = "/sounds/shapes/daire.mp3", Description = "Yuvarlak şekil." },
                new ShapeViewModel { Name = "Üçgen", ImagePath = "/images/shapes/ucgen.jpg", SoundPath = "/sounds/shapes/ucgen.mp3", Description = "Üç kenarı olan şekil." },
                new ShapeViewModel { Name = "Yıldız", ImagePath = "/images/shapes/yildiz.jpg", SoundPath = "/sounds/shapes/yildiz.mp3", Description = "Parlak ve köşeli şekil." },
                new ShapeViewModel { Name = "Altıgen", ImagePath = "/images/shapes/altigen.jpg", SoundPath = "/sounds/shapes/altigen.mp3", Description = "Altı kenarı olan şekil." },
                new ShapeViewModel { Name = "Beşgen", ImagePath = "/images/shapes/besgen.jpg", SoundPath = "/sounds/shapes/besgen.mp3", Description = "Beş kenarı olan şekil." },
                new ShapeViewModel { Name = "Oval", ImagePath = "/images/shapes/oval.jpg", SoundPath = "/sounds/shapes/oval.mp3", Description = "Yumurta gibi yuvarlak şekil." }
            };
            return View(shapes);
        }

        public IActionResult Numbers()
        {
            var numbers = new List<NumberViewModel>
            {
                new NumberViewModel { Value = 0, Name = "Sıfır", Description = "Hiç yok", ImagePath = "/images/numbers/0.jpg", SoundPath = "/sounds/numbers/0.mp3" },
                new NumberViewModel { Value = 1, Name = "Bir", Description = "Tek", ImagePath = "/images/numbers/1.jpg", SoundPath = "/sounds/numbers/1.mp3" },
                new NumberViewModel { Value = 2, Name = "İki", Description = "Çift", ImagePath = "/images/numbers/2.jpg", SoundPath = "/sounds/numbers/2.mp3" },
                new NumberViewModel { Value = 3, Name = "Üç", Description = "Üçgen", ImagePath = "/images/numbers/3.jpg", SoundPath = "/sounds/numbers/3.mp3" },
                new NumberViewModel { Value = 4, Name = "Dört", Description = "Kare", ImagePath = "/images/numbers/4.jpg", SoundPath = "/sounds/numbers/4.mp3" },
                new NumberViewModel { Value = 5, Name = "Beş", Description = "Beşgen", ImagePath = "/images/numbers/5.jpg", SoundPath = "/sounds/numbers/5.mp3" },
                new NumberViewModel { Value = 6, Name = "Altı", Description = "Altıgen", ImagePath = "/images/numbers/6.jpg", SoundPath = "/sounds/numbers/6.mp3" },
                new NumberViewModel { Value = 7, Name = "Yedi", Description = "Yıldız", ImagePath = "/images/numbers/7.jpg", SoundPath = "/sounds/numbers/7.mp3" },
                new NumberViewModel { Value = 8, Name = "Sekiz", Description = "Sekizgen", ImagePath = "/images/numbers/8.jpg", SoundPath = "/sounds/numbers/8.mp3" },
                new NumberViewModel { Value = 9, Name = "Dokuz", Description = "Dokuzgen", ImagePath = "/images/numbers/9.jpg", SoundPath = "/sounds/numbers/9.mp3" }
            };

            // Kullanıcı giriş yapmışsa, ilerleme bilgilerini getir
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.NumberProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.NumberValue, p => p.Progress);

                // Her sayı için ilerleme bilgisini ekle
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
                new TaleViewModel { Title = "Tavşan ve Kaplumbağa Masalı", Url = "https://www.youtube.com/watch?v=OtAhcLHMvw0" },
                new TaleViewModel { Title = "TOM VE JERRY", Url = "https://www.youtube.com/watch?v=GMniyQIc1eU" },
                new TaleViewModel { Title = "Kurt ve 7 Küçük Oğlak", Url = "https://www.youtube.com/watch?v=xqSQxPnL3SI&list=PL--gRX5NQW_fZRKOOfti4Nc7SOAl6VPcZ&index=2" },
                new TaleViewModel { Title = "Çirkin Ördek Yavrusu Masalı", Url = "https://www.youtube.com/watch?v=44MsdO8RQ4g" },
                new TaleViewModel { Title = "ÇALIŞKAN PASTACI", Url = "https://www.youtube.com/watch?v=Z7KCoIGVyqQ" },
                new TaleViewModel { Title = "Yıldızlarla süslenmiş terlikler", Url = "https://www.youtube.com/watch?v=tUNwPvxqYyg" },
                new TaleViewModel { Title = "Hansel ve Gretel", Url = "https://www.youtube.com/watch?v=jpRq4PSIg_U" },
                new TaleViewModel { Title = "Kırmızı Başlıklı Kız", Url = "https://www.youtube.com/watch?v=iM_Nd_Tf9XM" }
            };
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var progresses = _context.TaleProgress
                    .Where(p => p.UserId == userId)
                    .ToDictionary(p => p.TaleTitle, p => p.Progress);
                foreach (var tale in tales)
                {
                    if (progresses.ContainsKey(tale.Title))
                    {
                        tale.Progress = progresses[tale.Title];
                    }
                }
            }
            return View(tales);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTaleProgress([FromBody] TaleProgressInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Lütfen giriş yapın.", requireLogin = true });
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var progress = await _context.TaleProgress.FirstOrDefaultAsync(p => p.TaleTitle == model.TaleTitle && p.UserId == userId);
            if (progress == null)
            {
                progress = new TaleProgress
                {
                    TaleTitle = model.TaleTitle,
                    UserId = userId,
                    Progress = model.Progress,
                    LastInteraction = DateTime.UtcNow
                };
                _context.TaleProgress.Add(progress);
            }
            else
            {
                progress.Progress = model.Progress;
                progress.LastInteraction = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            return Json(new { success = true, progress = progress.Progress });
        }

        public class TaleProgressInputModel
        {
            public string TaleTitle { get; set; }
            public int Progress { get; set; }
        }

        public IActionResult TrafficSigns()
        {
            var signs = new List<TrafficSignViewModel>
            {
                new TrafficSignViewModel { Name = "Dur", ImagePath = "/images/traffic/dur.jpg", SoundPath = "/sounds/traffic/dur.mp3", Description = "Dur işareti: Trafikte durmanız gerektiğini belirtir." },
                new TrafficSignViewModel { Name = "Bisiklet", ImagePath = "/images/traffic/bisiklet.jpg", SoundPath = "/sounds/traffic/bisiklet.mp3", Description = "Bisiklet yolu işareti: Bisikletliler için ayrılmış yol." },
                new TrafficSignViewModel { Name = "Hastane", ImagePath = "/images/traffic/hastane.jpg", SoundPath = "/sounds/traffic/hastane.mp3", Description = "Hastane işareti: Yakında hastane olduğunu belirtir." },
                new TrafficSignViewModel { Name = "Işık", ImagePath = "/images/traffic/isik.jpg", SoundPath = "/sounds/traffic/isik.mp3", Description = "Trafik ışığı: Kırmızı, sarı, yeşil ışıklar ile trafiği düzenler." },
                new TrafficSignViewModel { Name = "Okul", ImagePath = "/images/traffic/okul.jpg", SoundPath = "/sounds/traffic/okul.mp3", Description = "Okul geçidi: Yakında okul olduğunu ve dikkatli olunması gerektiğini belirtir." },
                new TrafficSignViewModel { Name = "Park", ImagePath = "/images/traffic/park.jpg", SoundPath = "/sounds/traffic/park.mp3", Description = "Park yeri işareti: Araçların park edebileceği alanı gösterir." },
                new TrafficSignViewModel { Name = "Yaya", ImagePath = "/images/traffic/yaya.jpg", SoundPath = "/sounds/traffic/yaya.mp3", Description = "Yaya geçidi: Yayaların güvenle geçebileceği alanı belirtir." },
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
                },
                // Diğer görgü kuralları...
            };

            return View(manners);
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
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, progress = progress.Progress });
        }

        [HttpPost]
        public async Task<IActionResult> SaveShapeProgress([FromBody] ShapeProgressInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Lütfen giriş yapın.", requireLogin = true });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var progress = await _context.ShapeProgress.FirstOrDefaultAsync(p => p.ShapeName == model.ShapeName && p.UserId == userId);

            if (progress == null)
            {
                progress = new ShapeProgress
                {
                    ShapeName = model.ShapeName,
                    UserId = userId,
                    Progress = model.Progress,
                    InteractionCount = 1,
                    LastInteraction = DateTime.UtcNow
                };
                _context.ShapeProgress.Add(progress);
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

        public class ShapeProgressInputModel
        {
            public string ShapeName { get; set; }
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
            return View();
        }

        public IActionResult Exam(int id)
        {
            // Seviye 2 için farklı sorular
            if (id == 2)
            {
                var level2Questions = new List<ExamQuestionViewModel>
                {
                    new ExamQuestionViewModel { Question = "En hızlı koşan hayvan hangisidir?", Options = new List<string>{"Kaplan", "Çita", "Aslan", "Kanguru"}, CorrectIndex = 1 },
                    new ExamQuestionViewModel { Question = "Elma hangi renkte olabilir?", Options = new List<string>{"Sarı", "Kırmızı", "Yeşil", "Hepsi"}, CorrectIndex = 3 },
                    new ExamQuestionViewModel { Question = "5 + 3 kaç eder?", Options = new List<string>{"7", "8", "9", "6"}, CorrectIndex = 1 },
                    new ExamQuestionViewModel { Question = "Dikdörtgenin kaç kenarı vardır?", Options = new List<string>{"3", "4", "5", "6"}, CorrectIndex = 1 },
                    new ExamQuestionViewModel { Question = "Kuşlar ne yapabilir?", Options = new List<string>{"Uçar", "Yüzer", "Koşar", "Zıplar"}, CorrectIndex = 0 },
                    new ExamQuestionViewModel { Question = "Portakal hangi renktir?", Options = new List<string>{"Kırmızı", "Turuncu", "Mavi", "Siyah"}, CorrectIndex = 1 },
                    new ExamQuestionViewModel { Question = "6 - 2 kaç eder?", Options = new List<string>{"2", "3", "4", "5"}, CorrectIndex = 2 },
                    new ExamQuestionViewModel { Question = "Üçgenin kaç köşesi vardır?", Options = new List<string>{"2", "3", "4", "5"}, CorrectIndex = 1 },
                    new ExamQuestionViewModel { Question = "Karpuzun dışı genellikle hangi renktir?", Options = new List<string>{"Yeşil", "Kırmızı", "Sarı", "Mavi"}, CorrectIndex = 0 },
                    new ExamQuestionViewModel { Question = "Köpekler ne yapar?", Options = new List<string>{"Miyavlar", "Havlar", "Cik cik", "Möö"}, CorrectIndex = 1 }
                };
                ViewBag.Level = id;
                return View(level2Questions);
            }
            // Seviye 3 için farklı sorular
            if (id == 3)
            {
                var level3Questions = new List<ExamQuestionViewModel>
                {
                    new ExamQuestionViewModel { Question = "En büyük kara hayvanı hangisidir?", Options = new List<string>{"Fil", "Aslan", "Kanguru", "Köpek"}, CorrectIndex = 0 },
                    new ExamQuestionViewModel { Question = "Güneş hangi renkte görünür?", Options = new List<string>{"Mavi", "Sarı", "Kırmızı", "Yeşil"}, CorrectIndex = 1 },
                    new ExamQuestionViewModel { Question = "10 - 4 kaç eder?", Options = new List<string>{"5", "6", "7", "8"}, CorrectIndex = 1 },
                    new ExamQuestionViewModel { Question = "Beşgenin kaç kenarı vardır?", Options = new List<string>{"4", "5", "6", "7"}, CorrectIndex = 1 },
                    new ExamQuestionViewModel { Question = "Balıklar nerede yaşar?", Options = new List<string>{"Ağaçta", "Suda", "Toprakta", "Havada"}, CorrectIndex = 1 },
                    new ExamQuestionViewModel { Question = "Domates hangi renktir?", Options = new List<string>{"Kırmızı", "Mavi", "Sarı", "Yeşil"}, CorrectIndex = 0 },
                    new ExamQuestionViewModel { Question = "4 + 4 kaç eder?", Options = new List<string>{"6", "7", "8", "9"}, CorrectIndex = 2 },
                    new ExamQuestionViewModel { Question = "Dikdörtgenin karşılıklı kenarları nasıldır?", Options = new List<string>{"Eşit", "Farklı", "Kısa", "Daire"}, CorrectIndex = 0 },
                    new ExamQuestionViewModel { Question = "Kuşlar nasıl hareket eder?", Options = new List<string>{"Uçarak", "Yüzerek", "Sürünerek", "Zıplayarak"}, CorrectIndex = 0 },
                    new ExamQuestionViewModel { Question = "En küçük çift basamaklı sayı hangisidir?", Options = new List<string>{"9", "10", "11", "12"}, CorrectIndex = 1 }
                };
                ViewBag.Level = id;
                return View(level3Questions);
            }
            // Diğer seviyeler için eski sorular
            var allQuestions = new List<ExamQuestionViewModel>
            {
                new ExamQuestionViewModel { Question = "Kedi hangi hayvandır?", Options = new List<string>{"Miyavlayan", "Havlayan", "Uçan", "Yüzen"}, CorrectIndex = 0 },
                new ExamQuestionViewModel { Question = "Gökyüzü genellikle hangi renktir?", Options = new List<string>{"Kırmızı", "Mavi", "Sarı", "Yeşil"}, CorrectIndex = 1 },
                new ExamQuestionViewModel { Question = "2 + 2 kaç eder?", Options = new List<string>{"3", "4", "5", "2"}, CorrectIndex = 1 },
                new ExamQuestionViewModel { Question = "Daire kaç köşelidir?", Options = new List<string>{"0", "3", "4", "5"}, CorrectIndex = 0 },
                new ExamQuestionViewModel { Question = "Köpek hangi sesi çıkarır?", Options = new List<string>{"Miyav", "Hav hav", "Möö", "Cik cik"}, CorrectIndex = 1 },
                new ExamQuestionViewModel { Question = "Limon hangi renktir?", Options = new List<string>{"Sarı", "Mavi", "Kırmızı", "Yeşil"}, CorrectIndex = 0 },
                new ExamQuestionViewModel { Question = "3 üçgenin kaç kenarı vardır?", Options = new List<string>{"2", "3", "4", "5"}, CorrectIndex = 1 },
                new ExamQuestionViewModel { Question = "En büyük tek basamaklı sayı hangisidir?", Options = new List<string>{"7", "8", "9", "6"}, CorrectIndex = 2 },
                new ExamQuestionViewModel { Question = "Kırmızı ışıkta ne yapılır?", Options = new List<string>{"Geçilir", "Durulur", "Zıplanır", "Yüzülür"}, CorrectIndex = 1 },
                new ExamQuestionViewModel { Question = "Kare kaç kenarlıdır?", Options = new List<string>{"3", "4", "5", "6"}, CorrectIndex = 1 }
            };
            var rnd = new Random(id); // seviye bazlı karışıklık
            var questions = allQuestions.OrderBy(x => rnd.Next()).Take(10).ToList();
            ViewBag.Level = id;
            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> SaveExamResult([FromBody] ExamResultInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Lütfen giriş yapın.", requireLogin = true });
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = new ExamResult
            {
                ExamLevel = model.ExamLevel,
                UserId = userId,
                CorrectCount = model.CorrectCount,
                WrongCount = model.WrongCount,
                TotalQuestions = model.TotalQuestions,
                Score = model.Score,
                CompletedAt = DateTime.UtcNow
            };
            _context.ExamResults.Add(result);
            await _context.SaveChangesAsync();
            // Cevapları kaydet
            if (model.Answers != null)
            {
                for (int i = 0; i < model.Answers.Count; i++)
                {
                    var ans = model.Answers[i];
                    var answer = new ExamAnswer
                    {
                        ExamResultId = result.Id,
                        QuestionIndex = i,
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
            public int ExamLevel { get; set; }
            public int CorrectCount { get; set; }
            public int WrongCount { get; set; }
            public int TotalQuestions { get; set; }
            public double Score { get; set; }
            public List<ExamAnswerInputModel> Answers { get; set; }
        }

        public class ExamAnswerInputModel
        {
            public string QuestionText { get; set; }
            public string SelectedOption { get; set; }
            public string CorrectOption { get; set; }
            public bool IsCorrect { get; set; }
        }
    }
}