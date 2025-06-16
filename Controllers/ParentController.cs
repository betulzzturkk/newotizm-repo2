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

        public IActionResult Progress()
        {
            return View();
        }

        public IActionResult Development()
        {
            return View();
        }

        public IActionResult Guidance()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
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
                new AnimalViewModel { Name = "Kedi", Description = "Kediler oyuncu ve meraklıdır.", ImageUrl = "/images/animals/cat.jpg", SoundUrl = "/sounds/animals/kedi.mp3" },
                new AnimalViewModel { Name = "Köpek", Description = "Köpekler sadık ve sevimli hayvanlardır.", ImageUrl = "/images/animals/dog.jpg", SoundUrl = "/sounds/animals/kopek.mp3" },
                new AnimalViewModel { Name = "İnek", Description = "İnekler sakin ve süt veren hayvanlardır.", ImageUrl = "/images/animals/cow.jpg", SoundUrl = "/sounds/animals/inek.mp3" },
                new AnimalViewModel { Name = "Kuş", Description = "Kuşlar uçar ve güzel sesler çıkarır.", ImageUrl = "/images/animals/bird.jpg", SoundUrl = "/sounds/animals/kus.mp3" },
                new AnimalViewModel { Name = "At", Description = "Atlar hızlı koşar ve güçlüdür.", ImageUrl = "/images/animals/horse.jpg", SoundUrl = "/sounds/animals/at.mp3" },
                new AnimalViewModel { Name = "Aslan", Description = "Aslan ormanın kralıdır.", ImageUrl = "/images/animals/lion.jpg", SoundUrl = "/sounds/animals/aslan.mp3" },
                new AnimalViewModel { Name = "Tavuk", Description = "Tavuklar yumurta yapar ve gıdaklar.", ImageUrl = "/images/animals/chicken.jpg", SoundUrl = "/sounds/animals/tavuk.mp3" },
                new AnimalViewModel { Name = "Fil", Description = "Filler büyük ve güçlüdür.", ImageUrl = "/images/animals/elephant.jpg", SoundUrl = "/sounds/animals/fil.mp3" }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var progresses = await _context.AnimalProgress.Where(x => x.UserId == userId).ToListAsync();
                foreach (var animal in animals)
                {
                    var progress = progresses.FirstOrDefault(x => NormalizeAnimalName(x.AnimalName) == NormalizeAnimalName(animal.Name));
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
            var existing = _context.AnimalProgress.FirstOrDefault(x => x.UserId == userId && NormalizeAnimalName(x.AnimalName) == animalNameNorm);
            if (existing == null)
            {
                model.UserId = userId;
                model.AnimalName = animalNameNorm;
                model.LastUpdate = DateTime.UtcNow;
                model.CompletedAnimalCount = 1;
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
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        public async Task<IActionResult> Colors()
        {
            var colors = new List<AutismEducationPlatform.Web.Models.ViewModels.ColorViewModel>
            {
                new ColorViewModel { Name = "Kırmızı", Description = "Canlı ve dikkat çekici bir renktir.", ImagePath = "/images/colors/kirmizi.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Mavi", Description = "Gökyüzü ve denizin rengidir.", ImagePath = "/images/colors/mavi.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Sarı", Description = "Güneşin rengidir.", ImagePath = "/images/colors/sari.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Yeşil", Description = "Doğanın ve çimenlerin rengidir.", ImagePath = "/images/colors/yesil.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Turuncu", Description = "Portakalın rengidir.", ImagePath = "/images/colors/turuncu.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Mor", Description = "Çiçeklerin güzel rengidir.", ImagePath = "/images/colors/mor.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Pembe", Description = "Tatlı ve yumuşak bir renktir.", ImagePath = "/images/colors/pembe.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Kahverengi", Description = "Toprağın rengidir.", ImagePath = "/images/colors/kahverengi.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Siyah", Description = "Geceyi temsil eder.", ImagePath = "/images/colors/siyah.jpg", SoundPath = "" },
                new ColorViewModel { Name = "Beyaz", Description = "Saflığı ve temizliği simgeler.", ImagePath = "/images/colors/beyaz.jpg", SoundPath = "" },

                // Pastel Palet
                new ColorViewModel { Name = "Pastel Palet", Description = "Yumuşak ve huzur veren renklerdir. Genellikle çocuk odalarında ve anaokullarında tercih edilir.", ImagePath = "/images/colors/pastel-palet.jpg", SoundPath = "" },
                // Canlı Palet
                new ColorViewModel { Name = "Canlı Palet", Description = "Enerjik ve dikkat çekici renklerdir. Oyun alanlarında ve etkinliklerde kullanılır.", ImagePath = "/images/colors/canli-palet.jpg", SoundPath = "" },
                // Doğal Palet
                new ColorViewModel { Name = "Doğal Palet", Description = "Doğadan ilham alan, göz yormayan renklerdir. Sınıf ve terapi ortamlarında tercih edilir.", ImagePath = "/images/colors/dogal-palet.jpg", SoundPath = "" }
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var progresses = await _context.ColorProgress.Where(x => x.UserId == userId).ToListAsync();
                foreach (var color in colors)
                {
                    var progress = progresses.FirstOrDefault(x => x.ColorName.ToLower() == color.Name.ToLower());
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

        public async Task<IActionResult> Numbers()
        {
            var numbers = new List<AutismEducationPlatform.Web.Models.ViewModels.NumberViewModel>
            {
                new NumberViewModel { Value = 0, ImagePath = "/images/numbers/0.jpg", SoundPath = "" },
                new NumberViewModel { Value = 1, ImagePath = "/images/numbers/1.jpg", SoundPath = "" },
                new NumberViewModel { Value = 2, ImagePath = "/images/numbers/2.jpg", SoundPath = "" },
                new NumberViewModel { Value = 3, ImagePath = "/images/numbers/3.jpg", SoundPath = "" },
                new NumberViewModel { Value = 4, ImagePath = "/images/numbers/4.jpg", SoundPath = "" },
                new NumberViewModel { Value = 5, ImagePath = "/images/numbers/5.jpg", SoundPath = "" },
                new NumberViewModel { Value = 6, ImagePath = "/images/numbers/6.jpg", SoundPath = "" },
                new NumberViewModel { Value = 7, ImagePath = "/images/numbers/7.jpg", SoundPath = "" },
                new NumberViewModel { Value = 8, ImagePath = "/images/numbers/8.jpg", SoundPath = "" },
                new NumberViewModel { Value = 9, ImagePath = "/images/numbers/9.jpg", SoundPath = "" },

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
                var progresses = await _context.ShapeProgress.Where(x => x.UserId == userId).ToListAsync();
                foreach (var shape in shapes)
                {
                    var progress = progresses.FirstOrDefault(x => x.ShapeName.ToLower() == shape.Name.ToLower());
                    if (progress != null && shape.GetType().GetProperty("Progress") != null)
                        shape.GetType().GetProperty("Progress").SetValue(shape, progress.Progress);
                }
            }

            return View(shapes);
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

        public IActionResult Tales()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tales = _context.Tales.ToList();
            var progress = _context.TaleProgress
                .Where(p => p.UserId == userId)
                .ToDictionary(p => p.TaleId);

            var viewModel = tales.Select(t => new TaleViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Url = t.Url,
                Progress = progress.ContainsKey(t.Id) ? progress[t.Id].Progress : 0
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveTaleProgress([FromBody] TaleProgressModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tale = _context.Tales.FirstOrDefault(t => t.Title == model.TaleTitle);
            if (tale == null)
            {
                return Json(new { success = false, message = "Masal bulunamadı" });
            }

            var progress = _context.TaleProgress
                .FirstOrDefault(p => p.UserId == userId && p.TaleId == tale.Id);

            if (progress == null)
            {
                progress = new TaleProgress
                {
                    UserId = userId,
                    TaleId = tale.Id,
                    Progress = model.Progress
                };
                _context.TaleProgress.Add(progress);
            }
            else
            {
                progress.Progress = model.Progress;
            }

            _context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> SaveTrafficSignProgress([FromBody] TrafficSignProgressModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var progress = await _context.TrafficSignProgress
                .FirstOrDefaultAsync(p => p.UserId == userId && p.SignName == model.SignName);

            if (progress == null)
            {
                progress = new TrafficSignProgress
                {
                    UserId = userId,
                    SignName = model.SignName,
                    Progress = model.Progress
                };
                _context.TrafficSignProgress.Add(progress);
            }
            else
            {
                progress.Progress = model.Progress;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
} 