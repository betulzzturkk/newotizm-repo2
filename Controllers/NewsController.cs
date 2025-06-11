using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutismEducationPlatform.Web.Data;
using AutismEducationPlatform.Web.Models;

namespace AutismEducationPlatform.Web.Controllers
{
    [Authorize(Roles = "Parent")]
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var news = await _context.News
                .Where(n => n.IsActive)
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();

            return View(news);
        }
    }
} 