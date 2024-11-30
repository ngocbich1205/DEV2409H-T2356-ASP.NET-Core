using lab09.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        /// <summary> 
        /// Code logic để hiển thị thông tin giỏ hàng;  
        /// Dữ liệu giỏ hàng trong session cart 
        /// </summary> 
        /// <returns></returns> 
        public IActionResult Orders()
        {
            if (HttpContext.Session.GetString("Member") == null)
            {
                //return RedirectToAction("Index", "CustomerMember"); 
                return Redirect("/customermember/index/?url=/cart/orders");
                // nếu người dùng chưa đăng nhập 
            }
            else
            {
                var dataMember =
               JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("Member")); ViewBag.Customer = dataMember;
                float total = 0;
                foreach (var item in carts)
                {
                    total += item.Quantity * item.Price;
                }
                ViewBag.total = total;
                // Phương thức thanh toán 
                var dataPay = _context.PaymentMethods.ToList();
                ViewData["IdPayment"] = new SelectList(dataPay, "Id", "Name", 1);
            }
            return View(carts);
        }
        /// <summary> 
        /// Khi người dùng click vào nút thanh toán: 
        /// - Thực hiện thêm dữ liệu vào bảng Orders, OrderDetails 
        /// - Giải phóng session cart 
        /// </summary> 
        /// <param name="form"></param> 
        /// <returns></returns> 
        public async Task<IActionResult> OrderPay(IFormCollection form)
        {
            try
            {
                // Thêm bảng orders 
                var order = new Order();
                order.NameReciver = form["NameReciver"];
                order.Email = form["Email"];
                order.Phone = form["Phone"];
                order.Address = form["Address"];
                order.Notes = form["Notes"];
                order.Idpayment = long.Parse(form["Idpayment"]);
                order.OrdersDate = DateTime.Now;

                var dataMember =
JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("Member")); order.Idcustomer = dataMember.Id;
                decimal total = 0;
                foreach (var item in carts)
                {
                    total += item.Quantity * (decimal)item.Price;
                }
                order.TotalMoney = total;
                // tạo orderId 
                var strOrderId = "DH";

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd.HH-mm-ss.fff"); strOrderId += "." + timestamp;
                order.Idorders = strOrderId;
                _context.Add(order);
                await _context.SaveChangesAsync();
                // Lấy id bảng orders 
                var dataOrder = _context.Orders.OrderByDescending(x => x.Id).FirstOrDefault();
                foreach (var item in carts)
                {
                    Ordersdetail od = new Ordersdetail();
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

    }

}
