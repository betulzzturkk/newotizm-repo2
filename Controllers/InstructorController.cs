using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutismEducationPlatform.Web.Models;
using AutismEducationPlatform.Web.Models.ViewModels;
using AutismEducationPlatform.Web.Data;
using System.Security.Claims;
using System.Linq;

namespace AutismEducationPlatform.Web.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public InstructorController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var viewModel = new InstructorDashboardViewModel
            {
                InstructorInfo = new InstructorInfo
                {
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicture = user.ProfilePicture
                }
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Education()
        {
            var courses = await _context.Courses
                .Include(c => c.Instructor)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View(courses);
        }

        public IActionResult Courses() => View();

        public IActionResult Messages() => View();

        public IActionResult Profile() => View();

        public IActionResult AddCourse() => View(new CourseViewModel());

        [HttpPost]
        public IActionResult AddCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Dashboard));
            }
            return View(model);
        }

        public IActionResult EditCourse(int id) => View();

        public IActionResult DeleteCourse(int id) => RedirectToAction(nameof(Dashboard));

        public IActionResult StudentList() => View();

        public IActionResult Information() => View();

        public IActionResult ProgressReport() => View();

        public IActionResult Feedback() => View();

        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var students = await _context.Students
                .Where(s => s.InstructorId == user.Id)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            ViewBag.Students = students;

            return View(new StudentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent([FromForm] StudentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    
                    return Json(new { success = false, message = "Lütfen tüm zorunlu alanları doldurun.", errors = errors });
                }

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Json(new { success = false, message = "Oturum süresi dolmuş. Lütfen tekrar giriş yapın." });
                }

                var student = new Student
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Age = model.Age,
                    Gender = model.Gender,
                    Diagnosis = model.Diagnosis,
                    Hobbies = model.Hobbies,
                    Notes = model.Notes,
                    InstructorId = user.Id,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Öğrenci başarıyla eklendi.", redirectUrl = Url.Action("Students") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }

        public async Task<IActionResult> Students()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var students = await _context.Students
                .Where(s => s.InstructorId == user.Id)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return View(students);
        }

        public async Task<IActionResult> EditStudent(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id && s.InstructorId == user.Id);

            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                Age = student.Age,
                Gender = student.Gender,
                Diagnosis = student.Diagnosis,
                Hobbies = student.Hobbies,
                Notes = student.Notes,
                InstructorInfo = new InstructorInfo
                {
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicture = user.ProfilePicture
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Id == model.Id && s.InstructorId == user.Id);

                if (student == null)
                {
                    return NotFound();
                }

                student.Name = model.Name;
                student.Surname = model.Surname;
                student.Age = model.Age;
                student.Gender = model.Gender;
                student.Diagnosis = model.Diagnosis;
                student.Hobbies = model.Hobbies;
                student.Notes = model.Notes;
                student.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Öğrenci başarıyla güncellendi.";
                return RedirectToAction(nameof(Students));
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteStudent(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id && s.InstructorId == user.Id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Öğrenci başarıyla silindi.";
            return RedirectToAction(nameof(Students));
        }

        [HttpGet]
        public async Task<IActionResult> SeedCourses()
        {
            if (_context.Courses.Any())
            {
                return Content("Kurslar zaten eklenmiş.");
            }

            var courses = new List<Course>
            {
                new Course { Name = "Hayvanları Tanıyalım", Description = "Çocukların hayvanları tanımasını sağlayan eğlenceli bir kurs", Category = "Hayvanlar", DifficultyLevel = 3, ImageUrl = "/images/courses/animals.jpg", DurationMinutes = 30, Title = "Hayvanları Tanıyalım", IconClass = "fas fa-paw", BackgroundColor = "#f8fafc", IsActive = true },
                new Course { Name = "Renkleri Öğreniyorum", Description = "Temel renkleri öğreten interaktif aktiviteler", Category = "Renkler", DifficultyLevel = 2, ImageUrl = "/images/courses/colors.png", DurationMinutes = 25, Title = "Renkleri Öğreniyorum", IconClass = "fas fa-palette", BackgroundColor = "#f8fafc", IsActive = true },
                new Course { Name = "Sayıları Keşfedelim", Description = "1'den 10'a kadar sayıları öğreten eğlenceli oyunlar", Category = "Sayılar", DifficultyLevel = 4, ImageUrl = "/images/courses/numbers.jpg", DurationMinutes = 35, Title = "Sayıları Keşfedelim", IconClass = "fas fa-calculator", BackgroundColor = "#f8fafc", IsActive = true },
                new Course { Name = "Şekilleri Öğrenelim", Description = "Temel geometrik şekilleri tanıtan eğlenceli aktiviteler", Category = "Şekiller", DifficultyLevel = 2, ImageUrl = "/images/courses/shapes.jpg", DurationMinutes = 25, Title = "Şekilleri Öğrenelim", IconClass = "fas fa-shapes", BackgroundColor = "#f8fafc", IsActive = true },
                new Course { Name = "Duygularımız", Description = "Temel duyguları tanıma ve ifade etme aktiviteleri", Category = "Duygular", DifficultyLevel = 3, ImageUrl = "/images/courses/emotions.jpg", DurationMinutes = 30, Title = "Duygularımız", IconClass = "fas fa-smile", BackgroundColor = "#f8fafc", IsActive = true },
                new Course { Name = "Trafik İşaretlerini Öğrenelim", Description = "Temel trafik işaretlerini ve anlamlarını öğreten eğitici içerik", Category = "Trafik", DifficultyLevel = 4, ImageUrl = "/images/courses/traffic.jpg", DurationMinutes = 40, Title = "Trafik İşaretlerini Öğrenelim", IconClass = "fas fa-traffic-light", BackgroundColor = "#f8fafc", IsActive = true },
                new Course { Name = "Görgü Kuralları", Description = "Temel görgü kurallarını öğreten interaktif aktiviteler", Category = "Görgü Kuralları", DifficultyLevel = 3, ImageUrl = "/images/courses/manners.jpg", DurationMinutes = 35, Title = "Görgü Kuralları", IconClass = "fas fa-hands-helping", BackgroundColor = "#f8fafc", IsActive = true },
                new Course { Name = "Eğitici Masallar", Description = "Öğretici ve eğlenceli masallar koleksiyonu", Category = "Masallar", DifficultyLevel = 2, ImageUrl = "/images/courses/stories.jpg", DurationMinutes = 45, Title = "Eğitici Masallar", IconClass = "fas fa-book-open", BackgroundColor = "#f8fafc", IsActive = true },
                new Course { Name = "Eğitici Videolar", Description = "Çeşitli konularda eğitici video içerikleri", Category = "Videolar", DifficultyLevel = 1, ImageUrl = "/images/courses/videos.jpg", DurationMinutes = 50, Title = "Eğitici Videolar", IconClass = "fas fa-video", BackgroundColor = "#f8fafc", IsActive = true }
            };

            _context.Courses.AddRange(courses);
            await _context.SaveChangesAsync();
            return Content("Kurslar başarıyla eklendi.");
        }

        [HttpGet]
        public IActionResult ListAllCourses()
        {
            var courses = _context.Courses.ToList();
            return Json(courses);
        }

        [HttpGet]
        public async Task<IActionResult> FixCourseFields()
        {
            var courses = _context.Courses.ToList();
            foreach (var course in courses)
            {
                if (string.IsNullOrWhiteSpace(course.Title))
                    course.Title = course.Name;
                if (string.IsNullOrWhiteSpace(course.IconClass))
                {
                    switch (course.Category.ToLower())
                    {
                        case "hayvanlar": course.IconClass = "fas fa-paw"; break;
                        case "renkler": course.IconClass = "fas fa-palette"; break;
                        case "sayılar": course.IconClass = "fas fa-calculator"; break;
                        case "şekiller": course.IconClass = "fas fa-shapes"; break;
                        case "duygular": course.IconClass = "fas fa-smile"; break;
                        case "trafik": course.IconClass = "fas fa-traffic-light"; break;
                        case "görgü kuralları": course.IconClass = "fas fa-hands-helping"; break;
                        case "masallar": course.IconClass = "fas fa-book-open"; break;
                        case "videolar": course.IconClass = "fas fa-video"; break;
                        default: course.IconClass = "fas fa-book"; break;
                    }
                }
                if (string.IsNullOrWhiteSpace(course.BackgroundColor))
                    course.BackgroundColor = "#f8fafc";
            }
            _context.Courses.UpdateRange(courses);
            await _context.SaveChangesAsync();
            return Content("Kurs alanları güncellendi.");
        }

        public override ViewResult View() => SetInstructorLayout(base.View());

        public override ViewResult View(object? model) => SetInstructorLayout(base.View(model));

        public override ViewResult View(string? viewName) => SetInstructorLayout(base.View(viewName));

        public override ViewResult View(string? viewName, object? model) => SetInstructorLayout(base.View(viewName, model));

        private ViewResult SetInstructorLayout(ViewResult view)
        {
            ViewData["Layout"] = "~/Views/Shared/_InstructorLayout.cshtml";
            return view;
        }
    }
}