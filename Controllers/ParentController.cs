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
        public IActionResult AddChild()
        {
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
    }
} 