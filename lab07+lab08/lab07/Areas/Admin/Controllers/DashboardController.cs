using Microsoft.AspNetCore.Mvc;

namespace lab07.Areas.Admins.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
