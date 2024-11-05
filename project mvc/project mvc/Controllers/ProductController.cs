using Microsoft.AspNetCore.Mvc;

namespace project_mvc.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
