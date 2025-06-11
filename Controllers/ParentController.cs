using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutismEducationPlatform.Web.Models;
using AutismEducationPlatform.Web.Models.ViewModels;
using System.Text.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace AutismEducationPlatform.Web.Controllers
{
    [Authorize(Roles = "Parent")]
    public class ParentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ParentController> _logger;

        public ParentController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ParentController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        private async Task<ChildInfo> GetChildInfo(string userId)
        {
            // TODO: Veritabanından çocuk bilgilerini getir
            return new ChildInfo
                {
                    Name = "Ali Yılmaz",
                    Age = 8,
                Diagnosis = "Otizm Spektrum Bozukluğu",
                Interests = "Müzik, Resim, Hayvanlar",
                SpecialNeeds = "Görsel destekli eğitim materyalleri"
            };
        }

        private async Task<List<CourseViewModel>> GetActiveCourses(string userId)
        {
            // TODO: Veritabanından aktif kursları getir
            return new List<CourseViewModel>
            {
                new CourseViewModel
                {
                    Id = "1",
                    Title = "Hayvanları Tanıyalım",
                    Description = "Çiftlik ve ev hayvanlarını öğrenme",
                    Duration = 30,
                    Progress = 75
                },
                new CourseViewModel
                {
                    Id = "2",
                    Title = "Renkleri Öğreniyorum",
                    Description = "Temel renkleri tanıma ve ayırt etme",
                    Duration = 25,
                    Progress = 60
                },
                    new CourseViewModel
                    {
                    Id = "3",
                    Title = "Duygularımız",
                    Description = "Temel duygu durumlarını tanıma",
                    Duration = 20,
                    Progress = 40
                }
            };
        }

        private async Task<ProgressReport> GetProgressReport(string userId)
        {
            // TODO: Veritabanından ilerleme raporunu getir
            return new ProgressReport
            {
                Title = "Haftalık İlerleme Raporu",
                Content = "Bu hafta iletişim becerilerinde önemli gelişmeler kaydedildi.",
                Date = DateTime.Now.AddDays(-1),
                CompletedActivities = 15,
                SuccessRate = 85,
                TotalStudyTime = 12.5,
                WeeklyProgress = new List<WeeklyProgress>
                {
                    new WeeklyProgress 
                    { 
                        WeekStartDate = DateTime.Now.AddDays(-21),
                        CompletedLessons = 8,
                        StudyTime = 3.5,
                        SuccessRate = 75
                    },
                    new WeeklyProgress 
                    { 
                        WeekStartDate = DateTime.Now.AddDays(-14),
                        CompletedLessons = 10,
                        StudyTime = 4.0,
                        SuccessRate = 80
                    },
                    new WeeklyProgress 
                    { 
                        WeekStartDate = DateTime.Now.AddDays(-7),
                        CompletedLessons = 12,
                        StudyTime = 5.0,
                        SuccessRate = 85
                    }
                },
                CategoryProgress = new List<CategoryProgress>
                {
                    new CategoryProgress 
                    { 
                        CategoryName = "Hayvanlar",
                        TotalLessons = 10,
                        CompletedLessons = 8,
                        SuccessRate = 85,
                        Color = "#4CAF50"
                    },
                    new CategoryProgress 
                    { 
                        CategoryName = "Renkler",
                        TotalLessons = 8,
                        CompletedLessons = 6,
                        SuccessRate = 90,
                        Color = "#2196F3"
                    },
                    new CategoryProgress 
                    { 
                        CategoryName = "Duygular",
                        TotalLessons = 12,
                        CompletedLessons = 5,
                        SuccessRate = 75,
                        Color = "#FFC107"
                    }
                },
                RecentActivities = new List<LearningActivityLog>
                {
                    new LearningActivityLog 
                    { 
                        Date = DateTime.Now.AddHours(-2),
                        ActivityName = "Çiftlik Hayvanları",
                        Category = "Hayvanlar",
                        Score = 90,
                        Duration = 15,
                        Status = "Tamamlandı"
                    },
                    new LearningActivityLog 
                    { 
                        Date = DateTime.Now.AddHours(-4),
                        ActivityName = "Ana Renkler",
                        Category = "Renkler",
                        Score = 85,
                        Duration = 20,
                        Status = "Tamamlandı"
                    },
                    new LearningActivityLog 
                    { 
                        Date = DateTime.Now.AddDays(-1),
                        ActivityName = "Temel Duygular",
                        Category = "Duygular",
                        Score = 75,
                        Duration = 25,
                        Status = "Devam Ediyor"
                    }
                },
                Statistics = new OverallStatistics
                {
                    TotalLessonsAvailable = 30,
                    TotalLessonsCompleted = 19,
                    AverageSuccessRate = 83.3,
                    TotalStudyTimeHours = 12.5,
                    ConsecutiveDays = 5,
                    LastActivityDate = DateTime.Now.AddHours(-2),
                    TotalPoints = 1250,
                    CurrentLevel = "İleri Seviye"
                }
            };
        }

        private async Task<List<ProgressData>> GetProgressData(string userId)
        {
            // TODO: Veritabanından ilerleme verilerini getir
            return new List<ProgressData>
            {
                new ProgressData { Category = "İletişim", Value = 75, Color = "#4CAF50", Date = DateTime.Now.AddDays(-7) },
                new ProgressData { Category = "Sosyal", Value = 60, Color = "#2196F3", Date = DateTime.Now.AddDays(-6) },
                new ProgressData { Category = "Akademik", Value = 80, Color = "#FFC107", Date = DateTime.Now.AddDays(-5) },
                new ProgressData { Category = "Motor", Value = 70, Color = "#9C27B0", Date = DateTime.Now.AddDays(-4) }
            };
        }

        private async Task<List<Announcement>> GetAnnouncements()
        {
            // TODO: Veritabanından duyuruları getir
            return new List<Announcement>
            {
                new Announcement
                {
                    Title = "Yeni Eğitim Modülü",
                    Content = "Hayvanları Tanıyalım modülümüz yayına alınmıştır.",
                    Date = DateTime.Now.AddDays(-2),
                    Type = "info",
                    IsRead = false
                },
                new Announcement
                {
                    Title = "Platform Güncellemesi",
                    Content = "Yeni özellikler eklendi ve performans iyileştirmeleri yapıldı.",
                    Date = DateTime.Now.AddDays(-5),
                    Type = "update",
                    IsRead = true
                }
            };
        }

        private async Task<List<Message>> GetMessages(string userId)
        {
            // TODO: Veritabanından mesajları getir
            return new List<Message>
            {
                new Message
                {
                    Title = "Haftalık Değerlendirme",
                    Content = "Ali'nin bu haftaki performansı hakkında görüşmek isteriz.",
                    Date = DateTime.Now.AddDays(-1),
                    SenderName = "Ayşe Öğretmen",
                    IsRead = false
                },
                new Message
                {
                    Title = "Yeni Aktivite Önerisi",
                    Content = "Ali için yeni bir aktivite planı hazırladık. İncelemenizi rica ederiz.",
                    Date = DateTime.Now.AddDays(-3),
                    SenderName = "Mehmet Öğretmen",
                    IsRead = true
                }
            };
        }

        private async Task<List<FAQ>> GetFAQs()
        {
            // TODO: Veritabanından SSS'leri getir
            return new List<FAQ>
            {
                new FAQ
                {
                    Question = "Eğitimlere nasıl erişebilirim?",
                    Answer = "Ana sayfadaki 'Eğitimler' menüsünden tüm eğitim içeriklerine ulaşabilirsiniz.",
                    Category = "Eğitimler"
                },
                new FAQ
                {
                    Question = "İlerleme raporlarını nasıl görebilirim?",
                    Answer = "Çocuğumun Gelişimi sayfasından tüm ilerleme raporlarına erişebilirsiniz.",
                    Category = "Raporlar"
                }
            };
        }

        private ContactInfo GetContactInfo()
        {
            return new ContactInfo
            {
                Email = "destek@otizmegitim.com",
                Phone = "0850 123 4567",
                Address = "Merkez Mah. Eğitim Cad. No:1 İstanbul",
                SupportHours = "Hafta içi 09:00-18:00"
            };
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChildInfo(ChildInfo model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Lütfen tüm alanları doldurun." });
            }

            try
            {
                // TODO: Veritabanında çocuk bilgilerini güncelle
                return Json(new { success = true, message = "Bilgiler başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendFeedback(string message)
        {
            try
            {
                // TODO: Veritabanına geri bildirimi kaydet
                return Json(new { success = true, message = "Geri bildiriminiz için teşekkür ederiz." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }

        public async Task<IActionResult> Courses()
        {
            // Eğitimler sayfasına yönlendir
            return RedirectToAction("Index", "Course");
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("Kullanıcı bulunamadı");
                return NotFound();
            }

            _logger.LogInformation($"Mevcut kullanıcı bilgileri: FirstName={user.FirstName}, LastName={user.LastName}, ChildName={user.ChildName}, ChildAge={user.ChildAge}");

            var model = new ParentProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ChildName = user.ChildName,
                ChildAge = user.ChildAge,
                ChildDiagnosis = user.ChildDiagnosis,
                Address = user.Address
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(ParentProfileViewModel model)
        {
            _logger.LogInformation($"Gelen model bilgileri: FirstName={model.FirstName}, LastName={model.LastName}, ChildName={model.ChildName}, ChildAge={model.ChildAge}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model doğrulama hatası");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"Doğrulama hatası: {error.ErrorMessage}");
                }
                TempData["ErrorMessage"] = "Lütfen tüm zorunlu alanları doldurun.";
                return View("Profile", model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("Kullanıcı bulunamadı (güncelleme sırasında)");
                return NotFound();
            }

            try
            {
                _logger.LogInformation($"Güncellenecek kullanıcı bilgileri: ID={user.Id}, Email={user.Email}");

                // Mevcut değerleri logla
                _logger.LogInformation($"Mevcut değerler: FirstName={user.FirstName}, LastName={user.LastName}, ChildName={user.ChildName}, ChildAge={user.ChildAge}");

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.ChildName = model.ChildName;
                user.ChildAge = model.ChildAge;
                user.ChildDiagnosis = model.ChildDiagnosis;
                user.Address = model.Address;

                // Yeni değerleri logla
                _logger.LogInformation($"Yeni değerler: FirstName={user.FirstName}, LastName={user.LastName}, ChildName={user.ChildName}, ChildAge={user.ChildAge}");

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Kullanıcı başarıyla güncellendi");
                    TempData["StatusMessage"] = "Profil bilgileriniz başarıyla güncellendi.";

                    // Güncellenmiş kullanıcıyı kontrol et
                    var updatedUser = await _userManager.FindByIdAsync(user.Id);
                    _logger.LogInformation($"Güncellenmiş kullanıcı bilgileri: FirstName={updatedUser.FirstName}, LastName={updatedUser.LastName}, ChildName={updatedUser.ChildName}, ChildAge={updatedUser.ChildAge}");

                    return RedirectToAction(nameof(Profile));
                }

                _logger.LogWarning("Kullanıcı güncellenirken hatalar oluştu");
                foreach (var error in result.Errors)
                {
                    _logger.LogWarning($"Güncelleme hatası: {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı güncellenirken bir hata oluştu");
                ModelState.AddModelError(string.Empty, "Güncelleme sırasında bir hata oluştu: " + ex.Message);
                TempData["ErrorMessage"] = "Güncelleme sırasında bir hata oluştu: " + ex.Message;
            }

            return View("Profile", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNotificationSettings(bool emailNotifications, bool smsNotifications)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Burada bildirim ayarlarını veritabanına kaydedin
            // Örnek: user.NotificationSettings = new NotificationSettings { Email = emailNotifications, SMS = smsNotifications };
            // await _userManager.UpdateAsync(user);

            TempData["StatusMessage"] = "Bildirim ayarlarınız güncellendi.";
            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> MarkMessageAsRead(int messageId)
        {
            try
            {
                // TODO: Veritabanında mesajı okundu olarak işaretle
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MarkAnnouncementAsRead(int announcementId)
        {
            try
            {
                // TODO: Veritabanında duyuruyu okundu olarak işaretle
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
} 