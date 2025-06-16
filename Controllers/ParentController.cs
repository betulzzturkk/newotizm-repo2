using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AutismEducationPlatform.Web.Models;
using AutismEducationPlatform.Web.Models.ViewModels;
using AutismEducationPlatform.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Claims;
using AutismEducationPlatform.Web.Models.ViewModels; 


namespace AutismEducationPlatform.Web.Controllers
{
    [Authorize(Roles = "Parent")]
    public class ParentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ParentController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ParentProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ChildName = user.ChildName,
                ChildAge = user.ChildAge ?? 0,
                ProfileImageUrl = user.ProfileImageUrl ?? $"https://api.dicebear.com/7.x/avataaars/svg?seed={user.Email}",
                ActiveCourses = new List<CourseViewModel>(),
                LastProgressUpdate = DateTime.Now,
                RecentActivities = new List<ActivityViewModel>(),
                RecentMessages = new List<MessageViewModel>()
            };

            return View(model);
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ParentProfileViewModel
            {
                FirstName = user.FirstName ?? "VeliAdı",
                LastName = user.LastName ?? "VeliSoyadı",
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ChildName = user.ChildName ?? "Çocuk Adı",
                ChildAge = user.ChildAge ?? 8,
                ProfileImageUrl = user.ProfilePicture ?? $"https://api.dicebear.com/7.x/thumbs/svg?seed={new Random().Next(1, 1000)}",
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ParentProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Name = $"{model.FirstName} {model.LastName}";
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.ChildName = model.ChildName;
            user.ChildAge = model.ChildAge;
            user.ProfilePicture = model.ProfileImageUrl;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["ProfileSuccess"] = "Profiliniz başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Progress()
        {
            if (User?.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var animalProgress = await _context.AnimalProgress
                .Where(ap => ap.UserId == userId)
                .ToListAsync();

            var colorProgress = await _context.ColorProgress
                .Where(cp => cp.UserId == userId)
                .ToListAsync();

            var shapeProgress = await _context.ShapeProgress
                .Where(sp => sp.UserId == userId)
                .ToListAsync();

            var taleProgress = await _context.TaleProgress
                .Where(tp => tp.UserId == userId)
                .ToListAsync();

            var trafficSignProgress = await _context.TrafficSignProgress
                .Where(tsp => tsp.UserId == userId)
                .ToListAsync();

            var mannerProgress = await _context.MannerProgress
                .Where(mp => mp.UserId == userId)
                .ToListAsync();

            var numberProgress = await _context.NumberProgress
                .Where(np => np.UserId == userId)
                .ToListAsync();

            var examResults = await _context.ExamResults
                .Where(er => er.UserId == userId)
                .OrderByDescending(er => er.CompletedAt)
                .ToListAsync();

            // Son aktivite tarihini bul
            var lastActivity = new[]
            {
                animalProgress.Any() ? animalProgress.Max(ap => ap.LastInteraction) : DateTime.MinValue,
                colorProgress.Any() ? colorProgress.Max(cp => cp.LastInteraction) : DateTime.MinValue,
                shapeProgress.Any() ? shapeProgress.Max(sp => sp.LastInteraction) : DateTime.MinValue,
                taleProgress.Any() ? taleProgress.Max(tp => tp.LastInteraction) : DateTime.MinValue,
                trafficSignProgress.Any() ? trafficSignProgress.Max(tsp => tsp.LastInteraction) : DateTime.MinValue,
                mannerProgress.Any() ? mannerProgress.Max(mp => mp.LastInteraction) : DateTime.MinValue,
                numberProgress.Any() ? numberProgress.Max(np => np.LastInteraction) : DateTime.MinValue,
                examResults.Any() ? examResults.Max(er => er.CompletedAt) : DateTime.MinValue
            }.Max();

            var model = new ProgressViewModel
            {
                ChildName = user.ChildName ?? "İsimsiz Çocuk",
                ChildAge = user.ChildAge ?? 0,
                ChildDiagnosis = user.ChildDiagnosis,
                LastActivity = lastActivity,
                LastCompletedCourses = new List<CourseProgressViewModel>(),
                Exams = new List<ExamProgressViewModel>()
            };

            // Hayvanlar ilerlemesi
            model.AnimalProgress = animalProgress.Select(ap => new AnimalProgressViewModel
            {
                AnimalId = ap.Id,
                AnimalName = ap.AnimalName,
                ProgressPercentage = ap.Progress,
                LastInteraction = ap.LastInteraction
            }).ToList();

            // Renkler ilerlemesi
            model.ColorProgress = colorProgress.Select(cp => new ColorProgressViewModel
            {
                ColorName = cp.ColorName,
                ProgressPercentage = cp.Progress,
                LastInteraction = cp.LastInteraction
            }).ToList();

            // Sayılar ilerlemesi
            model.NumberProgress = numberProgress.Select(np => new NumberProgressViewModel
            {
                NumberValue = np.NumberValue,
                ProgressPercentage = np.Progress,
                LastInteraction = np.LastInteraction
            }).ToList();

            // Şekiller ilerlemesi
            model.ShapeProgress = shapeProgress.Select(sp => new ShapeProgressViewModel
            {
                ShapeName = sp.ShapeName,
                ProgressPercentage = sp.Progress,
                LastInteraction = sp.LastInteraction
            }).ToList();

            // Trafik işaretleri ilerlemesi
            model.TrafficSignProgress = trafficSignProgress.Select(tsp => new TrafficSignProgressViewModel
            {
                TrafficSignId = tsp.TrafficSignId,
                SignName = tsp.SignName,
                ProgressPercentage = tsp.Progress,
                LastInteraction = tsp.LastInteraction
            }).ToList();

            // Görgü kuralları ilerlemesi
            model.MannerProgress = mannerProgress.Select(mp => new MannerProgressViewModel
            {
                MannerName = mp.MannerName,
                ProgressPercentage = mp.Progress,
                LastInteraction = mp.LastInteraction
            }).ToList();

            // Masallar ilerlemesi
            model.TaleProgress = taleProgress.Select(tp => new TaleProgressViewModel
            {
                TaleId = tp.Id,
                TaleTitle = tp.Tale?.Title ?? "İsimsiz Hikaye",
                ProgressPercentage = tp.ProgressPercentage,
                LastInteraction = tp.LastInteraction
            }).ToList();

            // Sınav sonuçlarını ekle
            model.Exams.AddRange(examResults.Select(er => new ExamProgressViewModel
            {
                ExamId = er.Id,
                ExamName = $"Sınav {er.ExamLevel}",
                ExamLevel = er.ExamLevel,
                ExamDate = er.CompletedAt,
                Score = (int)er.Score,
                CorrectCount = er.CorrectCount,
                WrongCount = er.WrongCount,
                TotalQuestions = er.TotalQuestions
            }));

            return View(model);
        }

        public async Task<IActionResult> Guidance()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new GuidanceViewModel();

            // Çocuk bilgilerini getir
            var child = await _context.Children
                .Where(c => c.ParentId == user.Id)
                .FirstOrDefaultAsync();

            if (child != null)
            {
                model.ChildName = child.Name;
                model.ChildAge = child.Age;
                model.ChildDiagnosis = child.Diagnosis;
            }

            // Eğitim ilerlemelerini getir
            var courseProgress = await _context.CourseProgress
                .Where(x => x.UserId == user.Id)
                .ToListAsync();

            var courseProgressViewModel = courseProgress.Select(p => new CourseProgressViewModel
            {
                CourseName = p.Course?.Name ?? "İsimsiz Eğitim",
                ProgressPercentage = p.ProgressPercentage,
                LastInteraction = p.LastInteraction,
                CompletedActivities = p.CompletedActivities,
                TotalActivities = p.TotalActivities
            }).ToList();

            // Sınav sonuçlarını getir
            var examResults = await _context.ExamResults
                .Where(x => x.UserId == user.Id)
                .OrderByDescending(x => x.CompletedAt)
                .Select(x => new AutismEducationPlatform.Web.Models.ViewModels.ExamProgressViewModel
                {
                    ExamLevel = x.ExamLevel,
                    Score = (int)x.Score,
                    CorrectCount = x.CorrectCount,
                    WrongCount = x.WrongCount,
                    TotalQuestions = x.TotalQuestions,
                    ExamDate = x.CompletedAt
                })
                .ToListAsync();

            model.CourseProgress = courseProgressViewModel;
            model.ExamResults = examResults;

            return View(model);
        }

        public IActionResult News()
        {
            var news = new List<News>
            {
                new News
                {
                    Id = 1,
                    Title = "Otizm Farkındalık Ayı",
                    Content = "Nisan ayı boyunca düzenlenecek etkinlikler ve seminerler hakkında bilgi almak için tıklayın.",
                    Summary = "Nisan ayı boyunca düzenlenecek etkinlikler ve seminerler.",
                    Category = "Etkinlik",
                    PublishDate = DateTime.Now.AddDays(-2),
                    ImageUrl = "/images/news/autism-awareness.jpg",
                    IsAnnouncement = true,
                    Link = "https://www.autismawareness.com"
                },
                new News
                {
                    Id = 2,
                    Title = "Yeni Eğitim Programı",
                    Content = "Çocuğunuzun gelişimine katkı sağlayacak yeni eğitim programımız başlıyor.",
                    Summary = "Yeni eğitim programı detayları.",
                    Category = "Eğitim",
                    PublishDate = DateTime.Now.AddDays(-5),
                    ImageUrl = "/images/news/education.jpg",
                    IsAnnouncement = false,
                    Link = "https://www.education.com"
                },
                new News
                {
                    Id = 3,
                    Title = "Aile Destek Grubu Toplantısı",
                    Content = "Bu hafta sonu düzenlenecek aile destek grubu toplantısına davetlisiniz.",
                    Summary = "Aile destek grubu toplantısı detayları.",
                    Category = "Toplantı",
                    PublishDate = DateTime.Now.AddDays(-1),
                    ImageUrl = "/images/news/support-group.jpg",
                    IsAnnouncement = true,
                    Link = "https://www.supportgroup.com"
                }
            };

            return View(news);
        }

        public IActionResult Activities()
        {
            return View();
        }

        public IActionResult Messages()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }

        public async Task<IActionResult> Children()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var children = await _context.Children.Where(c => c.ParentId == user.Id).ToListAsync();
            var model = children.Select(c => new ChildViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Age = c.Age,
                Interests = c.Interests ?? ""
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddChild()
        {
            var user = await _userManager.GetUserAsync(User);
            var children = await _context.Children.Where(c => c.ParentId == user.Id).ToListAsync();
            ViewBag.Children = children.Select(c => new ChildViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Age = c.Age,
                Diagnosis = c.Diagnosis,
                Interests = c.Interests,
                SpecialNeeds = c.SpecialNeeds,
                CreatedAt = c.CreatedAt
            }).ToList();
            return View(new ChildViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddChild(ChildViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var child = new Child
            {
                Name = model.Name,
                Age = model.Age,
                Interests = model.Interests,
                ParentId = user.Id
            };
            _context.Children.Add(child);
            await _context.SaveChangesAsync();
            TempData["ChildSuccess"] = "Çocuk başarıyla eklendi.";
            return RedirectToAction("ChildInfo");
        }

        [HttpGet]
        public async Task<IActionResult> EditChild(int id)
        {
            var child = await _context.Children.FindAsync(id);
            if (child == null) return NotFound();
            var model = new ChildViewModel
            {
                Id = child.Id,
                Name = child.Name,
                Age = child.Age,
                Interests = child.Interests ?? ""
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditChild(ChildViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var child = await _context.Children.FindAsync(model.Id);
            if (child == null) return NotFound();
            child.Name = model.Name;
            child.Age = model.Age;
            child.Interests = model.Interests;
            child.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            TempData["ChildSuccess"] = "Çocuk bilgileri güncellendi.";
            return RedirectToAction("ChildInfo");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChild(int id)
        {
            var child = await _context.Children.FindAsync(id);
            if (child == null) return NotFound();
            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
            TempData["ChildSuccess"] = "Çocuk silindi.";
            return RedirectToAction("ChildInfo");
        }

        public async Task<IActionResult> ChildInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var children = await _context.Children.Where(c => c.ParentId == user.Id).ToListAsync();
            var model = children.Select(c => new ChildViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Age = c.Age,
                Interests = c.Interests ?? "",
                Diagnosis = c.Diagnosis,
                SpecialNeeds = c.SpecialNeeds,
                CreatedAt = c.CreatedAt,
                Notes = null,
            }).ToList();
            return View(model);
        }

        public IActionResult Education()
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

        private string NormalizeAnimalName(string name)
        {
            return name.ToLower()
                .Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("ö", "o").Replace("ç", "c")
                .Replace("İ", "i").Replace("Ğ", "g").Replace("Ü", "u").Replace("Ş", "s").Replace("Ö", "o").Replace("Ç", "c");
        }

        public async Task<IActionResult> Animals()
        {
            var animals = new List<AutismEducationPlatform.Web.Models.ViewModels.AnimalViewModel>
            {
                new AnimalViewModel { Id = 1, Name = "Kedi", Description = "Kediler oyuncu ve meraklıdır.", ImageUrl = "/images/animals/cat.jpg", SoundUrl = "/sounds/animals/kedi.mp3" },
                new AnimalViewModel { Id = 2, Name = "Köpek", Description = "Köpekler sadık ve sevimli hayvanlardır.", ImageUrl = "/images/animals/dog.jpg", SoundUrl = "/sounds/animals/kopek.mp3" },
                new AnimalViewModel { Id = 3, Name = "İnek", Description = "İnekler sakin ve süt veren hayvanlardır.", ImageUrl = "/images/animals/cow.jpg", SoundUrl = "/sounds/animals/inek.mp3" },
                new AnimalViewModel { Id = 4, Name = "Kuş", Description = "Kuşlar uçar ve güzel sesler çıkarır.", ImageUrl = "/images/animals/bird.jpg", SoundUrl = "/sounds/animals/kus.mp3" },
                new AnimalViewModel { Id = 5, Name = "At", Description = "Atlar hızlı koşar ve güçlüdür.", ImageUrl = "/images/animals/horse.jpg", SoundUrl = "/sounds/animals/at.mp3" },
                new AnimalViewModel { Id = 6, Name = "Aslan", Description = "Aslan ormanın kralıdır.", ImageUrl = "/images/animals/lion.jpg", SoundUrl = "/sounds/animals/aslan.mp3" },
                new AnimalViewModel { Id = 7, Name = "Tavuk", Description = "Tavuklar yumurta yapar ve gıdaklar.", ImageUrl = "/images/animals/chicken.jpg", SoundUrl = "/sounds/animals/tavuk.mp3" },
                new AnimalViewModel { Id = 8, Name = "Fil", Description = "Filler büyük ve güçlüdür.", ImageUrl = "/images/animals/elephant.jpg", SoundUrl = "/sounds/animals/fil.mp3" }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var progresses = await _context.AnimalProgress.Where(x => x.UserId == userId).ToListAsync();
                foreach (var animal in animals)
                {
                    var progress = progresses.FirstOrDefault(x => NormalizeAnimalName(x.AnimalName) == NormalizeAnimalName(animal.Name) && x.AnimalId == animal.Id);
                    if (progress != null)
                    {
                        animal.Progress = progress.Progress;
                        animal.CompletedAnimalCount = progress.CompletedAnimalCount;
                    }
                    else
                    {
                        animal.Progress = 0;
                        animal.CompletedAnimalCount = 0;
                    }
                }
            }
            else
            {
                foreach (var animal in animals)
                {
                    animal.Progress = 0;
                    animal.CompletedAnimalCount = 0;
                }
            }

            return View(animals);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAnimalProgress([FromBody] AnimalProgress model)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = _userManager.GetUserId(User);
            var animalNameNorm = NormalizeAnimalName(model.AnimalName);
            var progresses = await _context.AnimalProgress.Where(x => x.UserId == userId).ToListAsync();
            var existing = progresses.FirstOrDefault(x => NormalizeAnimalName(x.AnimalName) == animalNameNorm && x.AnimalId == model.AnimalId);
            if (existing == null)
            {
                model.UserId = userId;
                model.AnimalName = model.AnimalName;
                model.AnimalId = model.AnimalId;
                model.LastUpdate = DateTime.UtcNow;
                model.CompletedAnimalCount = model.Progress >= 100 ? 1 : 0;
                _context.AnimalProgress.Add(model);
            }
            else
            {
                existing.Progress = model.Progress;
                existing.LastUpdate = DateTime.UtcNow;
                if (model.Progress >= 100)
                    existing.CompletedAnimalCount = 1;
                _context.AnimalProgress.Update(existing);
            }

            // CourseProgress tablosunu güncelle
            var courseProgress = await _context.CourseProgress
                .FirstOrDefaultAsync(cp => cp.UserId == userId && cp.CourseName == "Hayvanlar");

            if (courseProgress == null)
            {
                courseProgress = new CourseProgress
                {
                    UserId = userId,
                    CourseId = 1, // Hayvanlar eğitimi için sabit ID
                    CourseName = "Hayvanlar",
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
                if (model.Progress >= 100)
                    courseProgress.CompletedActivities = 1;
                _context.CourseProgress.Update(courseProgress);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        public async Task<IActionResult> Colors()
        {
            var colors = new List<ColorViewModel>
            {
                new ColorViewModel { Name = "Kırmızı", Description = "Ateş ve tutku rengi.", ImagePath = "/images/colors/kirmizi.jpg", SoundPath = "/sounds/colors/kirmizi.mp3" },
                new ColorViewModel { Name = "Mavi", Description = "Gökyüzü ve deniz rengi.", ImagePath = "/images/colors/mavi.jpg", SoundPath = "/sounds/colors/mavi.mp3" },
                new ColorViewModel { Name = "Yeşil", Description = "Doğa ve huzur rengi.", ImagePath = "/images/colors/yesil.jpg", SoundPath = "/sounds/colors/yesil.mp3" },
                new ColorViewModel { Name = "Sarı", Description = "Güneş ve neşe rengi.", ImagePath = "/images/colors/sari.jpg", SoundPath = "/sounds/colors/sari.mp3" },
                new ColorViewModel { Name = "Mor", Description = "Asalet ve gizem rengi.", ImagePath = "/images/colors/mor.jpg", SoundPath = "/sounds/colors/mor.mp3" },
                new ColorViewModel { Name = "Turuncu", Description = "Enerji ve canlılık rengi.", ImagePath = "/images/colors/turuncu.jpg", SoundPath = "/sounds/colors/turuncu.mp3" },
                new ColorViewModel { Name = "Pembe", Description = "Sevgi ve şefkat rengi.", ImagePath = "/images/colors/pembe.jpg", SoundPath = "/sounds/colors/pembe.mp3" },
                new ColorViewModel { Name = "Kahverengi", Description = "Toprak ve güven rengi.", ImagePath = "/images/colors/kahverengi.jpg", SoundPath = "/sounds/colors/kahverengi.mp3" },
                new ColorViewModel { Name = "Siyah", Description = "Güç ve zarafet rengi.", ImagePath = "/images/colors/siyah.jpg", SoundPath = "/sounds/colors/siyah.mp3" },
                new ColorViewModel { Name = "Beyaz", Description = "Saflığı ve temizliği simgeler.", ImagePath = "/images/colors/beyaz.jpg", SoundPath = "/sounds/colors/beyaz.mp3" },

                // Paletler için ses yok
                new ColorViewModel { Name = "Pastel Palet", Description = "Yumuşak ve huzur veren renklerdir. Genellikle çocuk odalarında ve anaokullarında tercih edilir.", ImagePath = "/images/colors/pastel-palet.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Canlı Palet", Description = "Enerjik ve dikkat çekici renklerdir. Oyun alanlarında ve etkinliklerde kullanılır.", ImagePath = "/images/colors/canli-palet.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Doğal Palet", Description = "Doğadan ilham alan, göz yormayan renklerdir. Sınıf ve terapi ortamlarında tercih edilir.", ImagePath = "/images/colors/dogal-palet.jpg", SoundPath = "" }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var progresses = await _context.ColorProgress.Where(x => x.UserId == userId).ToListAsync();
                foreach (var color in colors)
                {
                    var normalizedColorName = NormalizeShapeName(color.Name);
                    var progress = progresses.FirstOrDefault(x => x.ColorName == normalizedColorName);
                    if (progress != null)
                        color.Progress = progress.Progress;
                    else
                        color.Progress = 0;
                }
            }
            else
            {
                foreach (var color in colors)
                    color.Progress = 0;
            }

            return View(colors);
        }

        [HttpPost]
        public async Task<IActionResult> SaveColorProgress([FromBody] ColorProgressInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = _userManager.GetUserId(User);
            var normalizedColorName = NormalizeShapeName(model.ColorName);
            var colorProgress = await _context.ColorProgress
                .FirstOrDefaultAsync(cp => cp.UserId == userId && cp.ColorName == normalizedColorName);

            if (colorProgress == null)
            {
                colorProgress = new ColorProgress
                {
                    UserId = userId,
                    ColorName = normalizedColorName,
                    Progress = model.Progress,
                    LastInteraction = DateTime.UtcNow,
                    InteractionCount = 1
                };
                _context.ColorProgress.Add(colorProgress);
            }
            else
            {
                colorProgress.Progress = model.Progress;
                colorProgress.LastInteraction = DateTime.UtcNow;
                colorProgress.InteractionCount++;
                _context.ColorProgress.Update(colorProgress);
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
            return Json(new { success = true });
        }

        public class ColorProgressInputModel
        {
            public string ColorName { get; set; }
            public int Progress { get; set; }
        }

        public async Task<IActionResult> Numbers()
        {
            var numbers = new List<AutismEducationPlatform.Web.Models.ViewModels.NumberViewModel>
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
                new NumberViewModel { Value = 9, Name = "Dokuz", Description = "Dokuzgen", ImagePath = "/images/numbers/9.jpg", SoundPath = "/sounds/numbers/9.mp3" },

                // Basit toplama işlemleri kartları
                new NumberViewModel { Name = "2 + 3", Description = "2 ile 3'ü toplarsak sonuç 5 olur.", ImagePath = "", SoundPath = "" },
                new NumberViewModel { Name = "4 + 1", Description = "4 ile 1'i toplarsak sonuç 5 olur.", ImagePath = "", SoundPath = "" },
                new NumberViewModel { Name = "5 + 2", Description = "5 ile 2'yi toplarsak sonuç 7 olur.", ImagePath = "", SoundPath = "" }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var progresses = await _context.NumberProgress.Where(x => x.UserId == userId).ToListAsync();
                foreach (var number in numbers)
                {
                    var progress = progresses.FirstOrDefault(x => x.NumberValue == number.Value);
                    if (progress != null)
                        number.Progress = progress.Progress;
                    else
                        number.Progress = 0;
                }
            }
            else
            {
                foreach (var number in numbers)
                    number.Progress = 0;
            }

            return View(numbers);
        }

        private string NormalizeShapeName(string name)
        {
            return name.ToLower()
                .Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("ö", "o").Replace("ç", "c")
                .Replace("İ", "i").Replace("Ğ", "g").Replace("Ü", "u").Replace("Ş", "s").Replace("Ö", "o").Replace("Ç", "c")
                .Replace(" ", "");
        }

        public async Task<IActionResult> Shapes()
        {
            var shapes = new List<AutismEducationPlatform.Web.Models.ViewModels.ShapeViewModel>
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

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var progresses = await _context.ShapeProgress
                    .Where(p => p.UserId == userId)
                    .ToListAsync();

                foreach (var shape in shapes)
                {
                    var normName = NormalizeShapeName(shape.Name);
                    var progress = progresses
                        .FirstOrDefault(p => NormalizeShapeName(p.ShapeName) == normName);
                    if (progress != null && shape.GetType().GetProperty("Progress") != null)
                        shape.GetType().GetProperty("Progress").SetValue(shape, progress.Progress);
                }
            }

            return View(shapes);
        }

        [HttpPost]
        public async Task<IActionResult> SaveShapeProgress([FromBody] ShapeProgressInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = _userManager.GetUserId(User);
            var shapeProgress = await _context.ShapeProgress
                .FirstOrDefaultAsync(sp => sp.UserId == userId && sp.ShapeName == model.ShapeName);

            if (shapeProgress == null)
            {
                shapeProgress = new ShapeProgress
                {
                    UserId = userId,
                    ShapeName = model.ShapeName,
                    Progress = model.Progress,
                    LastInteraction = DateTime.UtcNow
                };
                _context.ShapeProgress.Add(shapeProgress);
            }
            else
            {
                shapeProgress.Progress = model.Progress;
                shapeProgress.LastInteraction = DateTime.UtcNow;
                _context.ShapeProgress.Update(shapeProgress);
            }

            // CourseProgress tablosunu güncelle
            var courseProgress = await _context.CourseProgress
                .FirstOrDefaultAsync(cp => cp.UserId == userId && cp.CourseName == "Şekiller");

            if (courseProgress == null)
            {
                courseProgress = new CourseProgress
                {
                    UserId = userId,
                    CourseId = 3, // Şekiller eğitimi için sabit ID
                    CourseName = "Şekiller",
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
                if (model.Progress >= 100)
                    courseProgress.CompletedActivities = 1;
                _context.CourseProgress.Update(courseProgress);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        public IActionResult TrafficSigns()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var trafficSigns = _context.TrafficSigns.ToList();
            var progress = _context.TrafficSignProgress
                .Where(p => p.UserId == userId)
                .GroupBy(p => p.TrafficSignId)
                .ToDictionary(g => g.Key, g => g.First());

            var viewModel = trafficSigns.Select(t => new TrafficSignViewModel
            {
                Id = t.Id,
                Name = t.Name,
                ImageUrl = t.ImageUrl,
                ImagePath = t.ImageUrl,
                SoundPath = t.SoundPath,
                Description = t.Description,
                Progress = progress.ContainsKey(t.Id) ? progress[t.Id].Progress : 0
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Tales()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tales = await _context.Tales.ToListAsync();
            var taleProgress = await _context.TaleProgress
                .Where(tp => tp.UserId == user.Id)
                .ToListAsync();

            var viewModel = new TalesViewModel
            {
                Tales = tales.Select((tale, index) => new TaleViewModel
                {
                    Id = tale.Id,
                    Title = tale.Title,
                    Description = tale.Description,
                    VideoUrl = tale.VideoUrl,
                    ThumbnailUrl = tale.ThumbnailUrl,
                    ProgressPercentage = taleProgress.FirstOrDefault(tp => tp.TaleId == tale.Id)?.ProgressPercentage ?? 0,
                    LastInteraction = taleProgress.FirstOrDefault(tp => tp.TaleId == tale.Id)?.LastInteraction ?? DateTime.MinValue
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteTale(int taleId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var taleProgress = await _context.TaleProgress
                .FirstOrDefaultAsync(tp => tp.TaleId == taleId && tp.UserId == user.Id);

            if (taleProgress == null)
            {
                taleProgress = new TaleProgress
                {
                    TaleId = taleId,
                    UserId = user.Id,
                    ProgressPercentage = 100,
                    LastInteraction = DateTime.Now
                };
                _context.TaleProgress.Add(taleProgress);
            }
            else
            {
                taleProgress.ProgressPercentage = 100;
                taleProgress.LastInteraction = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult SaveTaleProgress([FromBody] TaleProgressModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = _userManager.GetUserId(User);
            var taleProgress = _context.TaleProgress
                .FirstOrDefault(tp => tp.UserId == userId && tp.TaleId == model.TaleId);

            if (taleProgress == null)
            {
                taleProgress = new TaleProgress
                {
                    UserId = userId,
                    TaleId = model.TaleId,
                    ProgressPercentage = model.Progress,
                    LastInteraction = DateTime.UtcNow
                };
                _context.TaleProgress.Add(taleProgress);
            }
            else
            {
                taleProgress.ProgressPercentage = model.Progress;
                taleProgress.LastInteraction = DateTime.UtcNow;
                _context.TaleProgress.Update(taleProgress);
            }

            // CourseProgress tablosunu güncelle
            var courseProgress = _context.CourseProgress
                .FirstOrDefault(cp => cp.UserId == userId && cp.CourseName == "Hikayeler");

            if (courseProgress == null)
            {
                courseProgress = new CourseProgress
                {
                    UserId = userId,
                    CourseId = 4, // Hikayeler eğitimi için sabit ID
                    CourseName = "Hikayeler",
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
                if (model.Progress >= 100)
                    courseProgress.CompletedActivities = 1;
                _context.CourseProgress.Update(courseProgress);
            }

            _context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> SaveTrafficSignProgress([FromBody] TrafficSignProgressModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = _userManager.GetUserId(User);
            var trafficSignProgress = await _context.TrafficSignProgress
                .FirstOrDefaultAsync(tsp => tsp.UserId == userId && tsp.TrafficSignId == model.TrafficSignId);

            if (trafficSignProgress == null)
            {
                trafficSignProgress = new TrafficSignProgress
                {
                    UserId = userId,
                    TrafficSignId = model.TrafficSignId,
                    SignName = model.SignName,
                    Progress = model.Progress,
                    LastInteraction = DateTime.UtcNow
                };
                _context.TrafficSignProgress.Add(trafficSignProgress);
            }
            else
            {
                trafficSignProgress.Progress = model.Progress;
                trafficSignProgress.LastInteraction = DateTime.UtcNow;
                _context.TrafficSignProgress.Update(trafficSignProgress);
            }

            // CourseProgress tablosunu güncelle
            var courseProgress = await _context.CourseProgress
                .FirstOrDefaultAsync(cp => cp.UserId == userId && cp.CourseName == "Trafik İşaretleri");

            if (courseProgress == null)
            {
                courseProgress = new CourseProgress
                {
                    UserId = userId,
                    CourseId = 5, // Trafik İşaretleri eğitimi için sabit ID
                    CourseName = "Trafik İşaretleri",
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
                if (model.Progress >= 100)
                    courseProgress.CompletedActivities = 1;
                _context.CourseProgress.Update(courseProgress);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Parent")]
        public async Task<IActionResult> SelfExpression()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var userId = user.Id;

            // Kullanıcıya özel ilerlemeleri çek
            var progresses = _context.MannerProgress
                .Where(p => p.UserId == userId)
                .ToDictionary(p => p.MannerName, p => p.Progress);

            var expressions = new List<MannersViewModel>
            {
                new MannersViewModel
                {
                    Title = "Teşekkür Etmek",
                    Description = "Teşekkür etmek, başkalarına karşı saygı göstermenin bir yoludur.",
                    ImagePath = "/images/manners/tesekkur.jpg",
                    Category = "Duygusal",
                    Color = "bg-primary",
                    Example = "Birisi size bir şey verdiğinde",
                    CorrectBehavior = "Teşekkür ederim demek",
                    SoundPath = "/sounds/manners/tesekkuretmek.mp3"
                },
                new MannersViewModel
                {
                    Title = "Özür Dilemek",
                    Description = "Yanlış bir davranış sonrası özür dilemek önemlidir.",
                    ImagePath = "/images/manners/ozur.jpg",
                    Category = "İletişim",
                    Color = "bg-info",
                    Example = "Birini üzdüğümde",
                    CorrectBehavior = "Özür dilerim demek",
                    SoundPath = "/sounds/manners/ozurdilemek.mp3"
                },
                new MannersViewModel
                {
                    Title = "Selamlaşmak",
                    Description = "İnsanlarla karşılaşınca selam vermek güzel bir davranıştır.",
                    ImagePath = "/images/manners/selam.jpg",
                    Category = "İletişim",
                    Color = "bg-warning",
                    Example = "Birini gördüğümde",
                    CorrectBehavior = "Selam vermek",
                    SoundPath = "/sounds/manners/selamlasmak.mp3"
                },
                new MannersViewModel
                {
                    Title = "Dinlemek",
                    Description = "Karşımızdakini dikkatlice dinlemek önemlidir.",
                    ImagePath = "/images/manners/dinle.jpg",
                    Category = "İletişim",
                    Color = "bg-success",
                    Example = "Biri konuşurken",
                    CorrectBehavior = "Dikkatlice dinlemek",
                    SoundPath = "/sounds/manners/dinlemek.mp3"
                },
                new MannersViewModel
                {
                    Title = "Kapıyı Çalmak",
                    Description = "Bir odaya girmeden önce kapıyı çalmak gerekir.",
                    ImagePath = "/images/manners/kapi.jpg",
                    Category = "İletişim",
                    Color = "bg-danger",
                    Example = "Bir odaya girmeden önce",
                    CorrectBehavior = "Kapıyı çalmak",
                    SoundPath = "/sounds/manners/kapiyicalmak.mp3"
                },
                new MannersViewModel
                {
                    Title = "Paylaşmak",
                    Description = "Arkadaşlarımızla eşyalarımızı paylaşmak güzel bir davranıştır.",
                    ImagePath = "/images/manners/paylasma.png",
                    Category = "İletişim",
                    Color = "bg-secondary",
                    Example = "Bir oyuncağım olduğunda",
                    CorrectBehavior = "Paylaşmak",
                    SoundPath = "/sounds/manners/paylasmak.mp3"
                }
            };

            // Progress değerlerini ekle
            foreach (var exp in expressions)
            {
                var key = exp.Title.ToLower().Replace(" ", "").Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("ö", "o").Replace("ç", "c");
                if (progresses.ContainsKey(key))
                {
                    exp.Progress = progresses[key];
                }
            }

            return View(expressions);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSelfExpressionProgress([FromBody] SaveMannerProgressInputModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = _userManager.GetUserId(User);
            var mannerProgress = await _context.MannerProgress
                .FirstOrDefaultAsync(mp => mp.UserId == userId && mp.MannerName == model.MannerName);

            if (mannerProgress == null)
            {
                mannerProgress = new MannerProgress
                {
                    UserId = userId,
                    MannerName = model.MannerName,
                    Progress = model.Progress,
                    LastInteraction = DateTime.UtcNow
                };
                _context.MannerProgress.Add(mannerProgress);
            }
            else
            {
                mannerProgress.Progress = model.Progress;
                mannerProgress.LastInteraction = DateTime.UtcNow;
                _context.MannerProgress.Update(mannerProgress);
            }

            // CourseProgress tablosunu güncelle
            var courseProgress = await _context.CourseProgress
                .FirstOrDefaultAsync(cp => cp.UserId == userId && cp.CourseName == "Görgü Kuralları");

            if (courseProgress == null)
            {
                courseProgress = new CourseProgress
                {
                    UserId = userId,
                    CourseId = 6, // Görgü Kuralları eğitimi için sabit ID
                    CourseName = "Görgü Kuralları",
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
                if (model.Progress >= 100)
                    courseProgress.CompletedActivities = 1;
                _context.CourseProgress.Update(courseProgress);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        public class SaveMannerProgressInputModel
        {
            public string MannerName { get; set; }
            public int Progress { get; set; }
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
                return RedirectToAction("Exams", "Course");
            }

            var questions = new List<object>();
            if (level == 2)
            {
                questions = GetLevel2Questions();
            }
            else if (level == 3)
            {
                questions = GetLevel3Questions();
            }
            else
            {
                questions = GetRandomQuestions();
            }

            ViewBag.Questions = questions;
            ViewBag.TotalQuestions = questions.Count;
            ViewBag.Level = level;

            return View();
        }

        private List<object> GetLevel2Questions()
        {
            return new List<object>
            {
                new {
                    content = "Aşağıdakilerden hangisi bir hayvan değildir?",
                    options = new[] { "Kedi", "Araba", "Köpek", "Kuş" },
                    correctAnswer = 1
                },
                new {
                    content = "Hangi renk gökyüzünün rengidir?",
                    options = new[] { "Kırmızı", "Mavi", "Yeşil", "Sarı" },
                    correctAnswer = 1
                },
                new {
                    content = "Aşağıdakilerden hangisi bir meyve değildir?",
                    options = new[] { "Elma", "Havuç", "Muz", "Portakal" },
                    correctAnswer = 1
                }
            };
        }

        private List<object> GetLevel3Questions()
        {
            return new List<object>
            {
                new {
                    content = "Aşağıdakilerden hangisi bir trafik işareti değildir?",
                    options = new[] { "Dur", "Yol Ver", "Okul", "Ağaç" },
                    correctAnswer = 3
                },
                new {
                    content = "Hangi sayı 5'ten büyüktür?",
                    options = new[] { "3", "4", "6", "2" },
                    correctAnswer = 2
                },
                new {
                    content = "Aşağıdakilerden hangisi bir geometrik şekil değildir?",
                    options = new[] { "Kare", "Üçgen", "Kalem", "Daire" },
                    correctAnswer = 2
                }
            };
        }

        private List<object> GetRandomQuestions()
        {
            var allQuestions = new List<object>();
            allQuestions.AddRange(GetLevel2Questions());
            allQuestions.AddRange(GetLevel3Questions());

            var random = new Random();
            return allQuestions.OrderBy(x => random.Next()).Take(3).ToList();
        }

        public IActionResult Resources()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
} 