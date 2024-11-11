using Microsoft.AspNetCore.Mvc;
using ss02.Models;

namespace ss02.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ViewData["messageVD"] = "Dữ liệu trong ViewData";
            ViewBag.messageVB = "Dữ liệu trong ViewBag";
            TempData["messageTD"] = "Dữ liệu lưu trong TempData";
            return View();
        }
        public IActionResult GetAllProducts ()
        {
           
            return View("Products");
        }
        public IActionResult GetProduct() {
            Product p = new Product
            {
                ProductId = 1,
                ProductName = "Trek s28-2016",
                YearRelease=2016,
                Price=79.00
            };
            ViewBag.product = p;
            ViewData["ProductVD"] = p;
            return View();
            }
    }
}
