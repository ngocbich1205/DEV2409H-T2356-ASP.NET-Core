using lab09.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace lab09.Controllers
{
    public class CartController : Controller, IActionFilter
    {
        private readonly DevXuongMocContext _context;
        private List<Cart> carts = new List<Cart>();
        public CartController(DevXuongMocContext context)
        {
            _context = context;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cartInSession = HttpContext.Session.GetString("My-Cart");
            if (cartInSession != null)
            {
                // neu cartInSession khong null thi gan du lieu cho bien carts
                //chuyen sang du lieu json
                carts = JsonConvert.DeserializeObject<List<Cart>>(cartInSession);
            }
            base.OnActionExecuting(context);
        }
        //code logic cho chuc nang them san pham vao gio hang
        public IActionResult Index()
        {
            float total = 0;
            foreach (var item in carts)
            {
                total += item.Quantity * item.Price;
            }
            ViewBag.total = total;//tong tien cua don hang
            return View(carts);
        }
        //code logic chuc nang them san pham vao gio hang
        public IActionResult Add(int id)
        {
            if (carts.Any(c => c.Id == id))// neu san pham nay da co trong gio hang
            {
                carts.Where(c => c.Id == id).First().Quantity += 1;// tang so luong
            }
            else // neu san pham chua co trong gio hang, them san pham ao gio hang
            {
                var p = _context.Products.Find(id);//tim san pham can mua trong bang san pham
                //tao moi mot san pham de them vao gio hang
                var item = new Cart()
                {
                    Id = p.Id,
                    Name = p.Title,
                    Price = (float)p.PriceNew.Value,
                    Quantity = 1,
                    Image = p.Image,
                    Total = (float)p.PriceNew.Value * 1
                };
                //them san pham vao gio hang
                carts.Add(item);
            }
            //luu carts vao session,can phai chuyen sang du lieu json
            HttpContext.Session.SetString("My-Cart", JsonConvert.SerializeObject(carts));
            return RedirectToAction("Index");
        }
        //code loic cho chuc nang xoa san pham trong gio hang
        public IActionResult Remove(int id)
        {
            if(carts.Any(c=>c.Id==id))
            {
                // tim san pham trong gio hang
                var item= carts.Where(c=>c.Id== id).First();
                //thuc hien xoa
                carts.Remove(item);
                //luu carts vao session, can phai chuyen sag du lieu json
                HttpContext.Session.SetString("My-Cart", JsonConvert.SerializeObject(carts));
            }
            return RedirectToAction("Index");
        }
        //code logic cho chuc nang cap nhat du lieu trong gio hang
        public IActionResult Update(int id, int quantity)
        {
            if (carts.Any(c => c.Id == id))
            {
                //tim san pham trong gio hang va cap nhat lai so luong moi
                carts.Where(c=>c.Id==id).First().Quantity = quantity;
                //luu carts vao sessio, can phai chuyen sang du lieu json
                HttpContext.Session.SetString("My-Cart",JsonConvert.SerializeObject(carts));
            }
            return RedirectToAction("Index");
        }
        //code logic cho chuc nang xoa het san pham trong gio hang
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("My-Cart");
            return RedirectToAction("Index");
        }
    }
}
