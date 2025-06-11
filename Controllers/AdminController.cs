using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutismEducationPlatform.Web.Models;
using AutismEducationPlatform.Web.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutismEducationPlatform.Web.Data;

namespace AutismEducationPlatform.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var courses = await _context.Courses.ToListAsync();
            var news = await _context.News.ToListAsync();

            var recentUsers = new List<UserViewModel>();
            foreach (var user in users.OrderByDescending(u => u.CreatedAt).Take(5))
            {
                var roles = await _userManager.GetRolesAsync(user);
                recentUsers.Add(new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "Belirlenmemiş",
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt
                });
            }

            var viewModel = new AdminDashboardViewModel
            {
                TotalUsers = users.Count,
                TotalCourses = courses.Count,
                ActiveUsers = users.Count(u => u.IsActive),
                TotalParents = users.Count(u => _userManager.IsInRoleAsync(u, "Parent").Result),
                TotalInstructors = users.Count(u => _userManager.IsInRoleAsync(u, "Instructor").Result),
                TotalNews = news.Count,
                RecentUsers = recentUsers,
                Courses = courses.Select(c => new CourseViewModel
                {
                    Id = c.Id.ToString(),
                    Title = c.Name,
                    StudentCount = 0, // Bu değer daha sonra eklenecek
                    Status = c.IsActive ? "Aktif" : "Pasif"
                }).ToList(),
                RecentNews = news.OrderByDescending(n => n.PublishDate)
                    .Take(5)
                    .Select(n => new NewsViewModel
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Category = n.Category,
                        IsActive = n.IsActive,
                        PublishDate = n.PublishDate.ToString("dd/MM/yyyy")
                    }).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "Belirlenmemiş",
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt
                });
            }

            return View(userViewModels);
        }

        public async Task<IActionResult> ToggleUserStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Users));
        }

        public IActionResult AddCourse()
        {
            return View(new CourseViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Name = model.Title,
                    Description = model.Description ?? "",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Title = model.Title,
                    IconClass = "fas fa-book", // Varsayılan ikon
                    BackgroundColor = "#007bff" // Varsayılan renk
                };

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> EditCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseViewModel
            {
                Id = course.Id.ToString(),
                Title = course.Name,
                Description = course.Description,
                Status = course.IsActive ? "Aktif" : "Pasif"
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = await _context.Courses.FindAsync(int.Parse(model.Id));
                if (course != null)
                {
                    course.Name = model.Title;
                    course.Description = model.Description ?? course.Description;
                    course.UpdatedAt = DateTime.UtcNow;
                    
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> News()
        {
            var news = await _context.News.ToListAsync();
            return View(news);
        }

        public IActionResult AddNews()
        {
            return View(new News());
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(News model)
        {
            if (ModelState.IsValid)
            {
                model.PublishDate = DateTime.UtcNow;
                model.IsActive = true;
                _context.News.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(News));
            }
            return View(model);
        }

        public async Task<IActionResult> EditNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> EditNews(News model)
        {
            if (ModelState.IsValid)
            {
                _context.News.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(News));
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(News));
        }
    }
} 