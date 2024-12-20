using DevXuongMoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace DevXuongMoc.Areas.CustomerUser.Controllers
{
    public class CartController : BaseController, IActionFilter
    {
        private readonly XuongMocContext _context;
        public List<Cart> carts = new List<Cart>();
        public CartController(XuongMocContext context)
        {
            _context = context;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cartInSession = HttpContext.Session.GetString("My-Cart");
            if (cartInSession != null)
            {
                // Nếu cartInSession không null thì gán dữ liệu cho biến carts
                // Chyển sang json
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
            ViewBag.total = total; // Tổng tiền của đơn hàng
            return View(carts);
        }
        public IActionResult Add(int id)
        {
            /// <summary>
            /// Code logic cho chức năng thêm sản phẩm và giỏ hàng
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>

            if (carts.Any(c => c.Id == id)) // Nếu sản phẩm đã cs trong giỏ hàng 
            {
                carts.Where(c => c.Id == id).First().Quantity += 1; // Tăng số lượng
            }
            else // Nếu sản phẩm chưa có trong giỏ hàng, thêm sản phẩm vào giỏ hàng
            {
                var p = _context.Products.Find(id); // Tìm sản phẩm cần mua trong bảng sản phẩm
                                                    // Tạo mới một sản phẩm đẻ thêm vào giỏ hàng
                var item = new Cart()
                {
                    Id = id,
                    Name = p.Title,
                    Price = (float)p.PriceNew.Value,
                    Quantity = 1,
                    Image = p.Image,
                    Total = (float)p.PriceNew.Value * 1,
                };
                // Thêm sản phẩm vào giỏ hàng
                carts.Add(item);
            }
            // Lưu carts vào sesson, cần pahir chuyển sang dữ liệu json
            HttpContext.Session.SetString("My-Cart", JsonConvert.SerializeObject(carts));
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            /// <summary>
            /// Code logic cho chức năng xóa sản phẩm và giỏ hàng
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>

            if (carts.Any(c => c.Id == id))
            {
                // Tìm sản phẩm tronf giỏ hàng
                var item = carts.Where(c => c.Id == id).First();
                // Thực hiện xóa
                carts.Remove(item);

                // Lưu carts vào session, cần phải chuyển dữ liệu sang json
                HttpContext.Session.SetString("My-Cart", JsonConvert.SerializeObject(carts));
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id, int quantity)
        {
            /// <summary>
            /// Code logic cho chức năng cập nhật sản phẩm và giỏ hàng
            /// </summary>
            /// <param name="id"></param>
            /// <param name="quantity"></param>
            /// <returns></returns>

            if (carts.Any(c => c.Id == id))
            {
                // Tìm sản phẩm trong giỏ hàng và cập nhật lại số lượng mới
                carts.Where(c => c.Id == id).First().Quantity = quantity;

                // Lưu carts vào session, cần phải chuyển sang dữ liệu json
                HttpContext.Session.SetString("My-Cart", JsonConvert.SerializeObject(carts));
            }
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            /// <summary>
            /// Code logic cho chức năng xóa sản phẩm và giỏ hàng
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>

            HttpContext.Session.Remove("My-Cart");
            return RedirectToAction("Index");
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
                // return RedirectToAction("Index", "CustomerMember");
                return Redirect("/customermember/index/?url=/cart/orders");
                // Nếu người dùng chưa đăng nhập
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

                // Phương thức thanh toán
                var dataPay = _context.PaymentMethods.ToList();
                ViewData["IdPayment"] = new SelectList(dataPay, "Id", "Name", 1);
            }
            return View(carts);
        }

        /// <summary>
        /// Khi người dùng click vào nút thanh toán
        /// - Thực hiện thêm dữ liệu vào bảng Orders, OrderDetails
        /// - Giải phóng session cart
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>    
        //public async Task<IActionResult> OrderPay(IFormCollection form)
        //{
        //    try
        //    {
        //        // Thêm bảng orders 
        //        var order = new DevXuongMoc.Models.Order();
        //        //order.Idorders = "ID"+ String.Format("{0:yymmddss}", DateTime.Now);
        //        order.Email = form["Email"];
        //        order.NameReciver = form["NameReciver"];
        //        order.Email = form["Email"];
        //        order.Phone = form["Phone"];
        //        order.Address = form["Address"];
        //        order.Notes = form["Notes"];
        //        order.Idpayment = long.Parse(form["IdPayment"]);
        //        order.OrdersDate = DateTime.Now;
        //        var dataMember =
        //       JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("Member")); order.Idcustomer = dataMember.Id;
        //        decimal total = 0;
        //        foreach (var item in carts)
        //        {
        //            total += item.Quantity * (decimal)item.Price;
        //        }
        //        order.TotalMoney = total;
        //        //// tạo orderId 
        //        //var strOrderId = "DH";

        //        //string timestamp = DateTime.Now.ToString("yyyy-MM-dd.HH-mm-ss.fff");
        //        //strOrderId += "." + timestamp;
        //        //order.Idorders = strOrderId;
        //        //_context.Add(order);
        //        //await _context.SaveChangesAsync();
        //        // Lấy id bảng orders 
        //        var dataOrder = _context.Orders.OrderByDescending(x => x.Id).FirstOrDefault();
        //        foreach (var item in carts)
        //        {
        //            Orderdetail od = new Orderdetail();
        //            od.Idorders = dataOrder.Id;
        //            od.Idproduct = item.Id;
        //            od.Qty = item.Quantity;
        //            od.Price = (decimal)item.Price;
        //            od.Total = (decimal)item.Total;
        //            od.ReturnQty = 0;
        //            _context.Add(od);
        //            await _context.SaveChangesAsync();
        //        }
        //        HttpContext.Session.Remove("My-Cart");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return View();
        //}
        public async Task<IActionResult> OrderPay(IFormCollection form)
        {
            try
            {
                // Validate session
                var sessionMember = HttpContext.Session.GetString("Member");
                if (string.IsNullOrEmpty(sessionMember))
                {
                    return Redirect("/customermember/index/?url=/cart/orders");
                }

                // Deserialize session data
                var dataMember = JsonConvert.DeserializeObject<Customer>(sessionMember);

                // Create new order
                var order = new Order
                {
                    NameReciver = form["NameReciver"],
                    Email = form["Email"],
                    Phone = form["Phone"],
                    Address = form["Address"],
                    Notes = form["Notes"],
                    Idpayment = long.Parse(form["Idpayment"]),
                    OrdersDate = DateTime.Now,
                    Idcustomer = dataMember.Id
                };

                decimal total = 0;
                foreach (var item in carts)
                {
                    total += item.Quantity * (decimal)item.Price;
                }
                order.TotalMoney = total;

                // Generate unique OrderId
                var strOrderId = "DH" + DateTime.Now.ToString("yyMMddss");
                order.Idorders = strOrderId;

                _context.Add(order);
                await _context.SaveChangesAsync();

                // Insert order details
                var dataOrder = _context.Orders.OrderByDescending(x => x.Id).FirstOrDefault();

                foreach (var item in carts)
                {
                    var od = new Orderdetail
                    {
                        Idord = dataOrder.Id,
                        Idproduct = item.Id,
                        Qty = item.Quantity,
                        Price = (decimal)item.Price,
                        Total = (decimal)item.Total,
                        ReturnQty = 0
                    };

                    _context.Add(od);
                }
                await _context.SaveChangesAsync();

                // Clear cart session
                HttpContext.Session.Remove("My-Cart");
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest("An error occurred during order processing.");
            }
            return View();
        }
    }
}
