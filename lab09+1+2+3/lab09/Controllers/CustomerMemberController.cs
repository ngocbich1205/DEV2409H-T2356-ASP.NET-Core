using lab09.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;


namespace lab09.Controllers
{
    public class CustomerMemberController : Controller
    {
        private readonly DevXuongMocContext _context;


        public CustomerMemberController(DevXuongMocContext context)
        {
            _context = context;
        }
        public IActionResult Index(string url)
        {
            if (HttpContext.Session.GetString("Member") != null)
            {
                var dataLogin = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("Member"));
                ViewBag.Customer = dataLogin;
            }
            ViewBag.UrlAction = url;
            return View();
        }
        /// <summary> 
        /// Xử lý chức năng khi người dùng click vào nút Registry  /// </summary> 
        /// <returns></returns> 
        [HttpPost]
        public IActionResult Registy(Customer model)
        {
            try
            {
                var pass = Utilities.Utils.GetSHA26Hash(model.Password);
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.Isactive = 1;
                _context.Add(model);
                _context.SaveChanges();
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorRegisty"] = "Lỗi đăng ký; " + ex.Message; return RedirectToAction("Index");
            }

        }
        /// <summary> 
        /// Chức năng đăng nhập 
        /// </summary> 
        /// <param name="model"></param> 
        /// <param name="urlAction"></param> 
        /// <returns></returns> 
        [HttpPost]
        public IActionResult Login(LoginUser model, string urlAction)
        {
            var pass = Utilities.Utils.GetSHA26Hash(model.Password);
            var data = _context.Customers.Where(x => x.Isactive == 1).Where(x => x.Username.Equals(model.UserOrEmail) ||
           x.Email.Equals(model.UserOrEmail)).FirstOrDefault(x => x.Password.Equals(pass)); var dataLogin = data.ToJson();
            if (data != null)
            {
                // Lưu session khi đăng nhập thành công 
                HttpContext.Session.SetString("Member", dataLogin);
                if (!string.IsNullOrEmpty(urlAction))
                {
                    return Redirect(urlAction);
                }
                return RedirectToAction("Index");
            }
            TempData["errorLogin"] = "Lỗi đăng nhập";
            return RedirectToAction("Index");
        }


    }
}
