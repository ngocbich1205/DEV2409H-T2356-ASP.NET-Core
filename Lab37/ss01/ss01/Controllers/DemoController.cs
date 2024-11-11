using Microsoft.AspNetCore.Mvc;

namespace ss01.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
