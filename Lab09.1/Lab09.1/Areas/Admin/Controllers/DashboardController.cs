using Lab09._1.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lab09.Areas.Admin.Controllers
{
    //[Area("Admin")]
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
