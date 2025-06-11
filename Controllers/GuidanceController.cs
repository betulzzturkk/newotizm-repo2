using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutismEducationPlatform.Web.Controllers
{
    [Authorize(Roles = "Parent")]
    public class GuidanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 