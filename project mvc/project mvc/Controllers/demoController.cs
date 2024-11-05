using Microsoft.AspNetCore.Mvc;

namespace project_mvc.Controllers
{
    public class demoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
