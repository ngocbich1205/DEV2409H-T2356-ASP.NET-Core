using Microsoft.AspNetCore.Mvc;

namespace ss02.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllProducts()
        {
            return View(Products);
        }

      
    }
}
