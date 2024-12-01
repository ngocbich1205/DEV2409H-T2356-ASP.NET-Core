
using lab09.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace DevXuongMoc.Controllers
{
    public class CartsController : Controller, IActionFilter
    {
        private readonly DevXuongMocContext _context;
        private List<Cart> carts = new List<Cart>();
        public CartsController(DevXuongMocContext context)
        {
            _context = context;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cartInSession = HttpContext.Session.GetString("My-Cart");
            if (cartInSession != null)
            {
                // nếu cartInSession không null thì gán dữ liệu cho biến carts
                // Chuyển sang dữ liệu json
                carts = JsonConvert.DeserializeObject<List<Cart>>(cartInSession);
            }
            base.OnActionExecuting(context);
        }
        // GET: CartController
        public IActionResult Index()
        {
            float total = 0;
            foreach (var item in carts)
            {
                total += item.Quantity * item.Price;
            }
            ViewBag.total = total;
            return View(carts);
        }

        // GET: CartController/Details/5
        public ActionResult Add(int id)
        {
            if (carts.Any(c => c.Id == id))// nếu sản phẩm này đã có trong giỏ hàng
            {
                carts.Where(c => c.Id == id).First().Quantity += 1; // tăng số lượng

            }
            else // Nếu sản phẩm chưa có trong giỏ hàng, thêm sản phẩm vào giỏ hàng
            {
                var p = _context.Products.Where(x => x.Id == id).DefaultIfEmpty().FirstOrDefault();// tìm sản phẩm cần mua trong bảng sản phẩm
                // tạo mới một sản phẩm để thêm vào giỏ hàng
                var item = new Cart()
                {
                    Id = p.Id,
                    Name = p.Title,
                    Price = (float)p.PriceNew.Value,
                    Quantity = 1,
                    Image = p.Image,
                    Total = (float)p.PriceNew.Value * 1,

                };
                // Thêm sản phẩm vào giỏ hàng
                carts.Add(item);
            }
            //Lưu carts vào session, cần phải chuyển sang dữ liệu json
            HttpContext.Session.SetString("My-Cart", JsonConvert.SerializeObject(carts));
            return RedirectToAction("Index");
        }

        // GET: CartController/Create
        public ActionResult Remove(int id)
        {
            if (carts.Any(c => c.Id == id))
            {
                //tìm sản phẩm trong giỏ hàng
                var item = carts.Where(c => c.Id == id).First();
                //Thực hiện xoá
                carts.Remove(item);
                // Lưu carts vào session , cần phải chuyển sang dữ liệu json
                HttpContext.Session.SetString("My_cart", JsonConvert.SerializeObject(carts));

            }
            return RedirectToAction("Index");
        }


        public IActionResult Update(int id, int quantity)
        {
            if (carts.Any(c => c.Id == id))
            {
                // tìm kiếm sản phẩm trong giỏ hnafg và cập nhật lại số lượng mới
                carts.Where(c => c.Id == id).First().Quantity = quantity;
                // lưu carts vào session , cần phải chuyển sang dữ liệu json
                HttpContext.Session.SetString("My-Cart", JsonConvert.SerializeObject(carts));

            }
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("My-Cart");
            return RedirectToAction("Index");
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// Code logic để hiển thị thông tin giỏ hàng
        /// Dữ liệu giỏ hàng trong session cart
        /// </summary>
        /// <returns></returns>


        public IActionResult Orders()
        {
            if (HttpContext.Session.GetString("Member") == null)
            {
                //return RedirectToAction("Index", "CustomerMember");
                return Redirect("/customermember/index/?url=/cart/orders");
                //nếu người dùng chưa đăng nhập
            }
            else
            {
                var dataMember = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("Member"));
                ViewBag.Customer = dataMember;

                float total = 0;
                foreach (var item in carts)
                {
                    total += item.Quantity * item.Price;
                }
                ViewBag.total = total;

                //Phương thức thanh toán
                var dataPay = _context.PaymentMethods.ToList();
                ViewData["IdPayment"] = new SelectList(dataPay, "Id", "Name", 1);
            }
            return View(carts);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OrderPay(IFormCollection form)
        {
            try
            {
                //Thêm bảng orders
                var order = new Order();
                order.NameReciver = form["NameReciver"];
                order.Email = form["Email"];
                order.Phone = form["Phone"];
                order.Address = form["Address"];
                order.Notes = form["Notes"];
                order.Idpayment = long.Parse(form["Idpayment"]);
                order.OrdersDate = DateTime.Now;

                var dataMember = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("Member"));
                order.Idcustomer = dataMember.Id;

                decimal total = 0;
                foreach (var item in carts)
                {
                    total += item.Quantity * (decimal)item.Price;
                }
                order.TotalMoney = total;

                //tạo orderId
                var strOrderId = "DH";

                string timestamp = DateTime.Now.ToString("yyMMddss");
                strOrderId += timestamp;
                order.Idorders = strOrderId;

                _context.Add(order);
                await _context.SaveChangesAsync();

                //lấy id bảng orders
                var dataOrder = _context.Orders.OrderByDescending(x => x.Id).FirstOrDefault();

                foreach (var item in carts)
                {
                    Orderdetail od = new Orderdetail();
                    od.Idord = dataOrder.Id;
                    od.Idproduct = item.Id;
                    od.Qty = item.Quantity;
                    od.Price = (decimal)item.Price;
                    od.Total = (decimal)item.Total;
                    od.ReturnQty = 0;

                    _context.Add(od);
                    await _context.SaveChangesAsync();
                }
                HttpContext.Session.Remove("My-Cart");
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
        public IActionResult OrderPay()
        {
            return View();
        }
    }
}
