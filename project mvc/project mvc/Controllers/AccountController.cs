using Microsoft.AspNetCore.Mvc;
using project_mvc.Models;

namespace project_mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            List<Account> accounts = new List<Account>
            {
                new Account()
                {
                    Id=1,Name="Hoàng Anh",
                    Email="anh@gmail.com",
                    Phone="0987654328",
                    Address="Hà Nội",
                    Avatar=Url.Content("~/images/avatar/avatar1.png"),
                    Gender=1,Bio="My name is small",
                    Birthday= new DateTime(1998,7,15)
                },
                new Account()
                {
                    Id=2,Name="Hoàng Anh",
                    Email="anh@gmail.com",
                    Phone="0987654328",
                    Address="Hà Nội",
                    Avatar=Url.Content("~/images/avatar/avatar2.jpg"),
                    Gender=1,Bio="My name is small",
                    Birthday= new DateTime(1998,7,15)
                },
            };
            ViewBag.accounts = accounts;
            return View();
        }
        //định nghĩa ủl và nam cho action
        [Route ("ho-so-cua-toi",Name ="profile")]
        public IActionResult Profile( int id)
        {
            List<Account> accounts = new List<Account>
            {
                new Account()
                {
                    Id=1,Name="Hoàng Anh",
                    Email="anh@gmail.com",
                    Phone="0987654328",
                    Address="Hà Nội",
                    Avatar=Url.Content("~/images/avatar/avatar1.png"),
                    Gender=1,Bio="My name is small",
                    Birthday= new DateTime(1998,7,15)
                },
                new Account()
                {
                    Id=2,Name="Hoàng Anh",
                    Email="anh@gmail.com",
                    Phone="0987654328",
                    Address="Hà Nội",
                    Avatar=Url.Content("~/images/avatar/avatar2.jpg"),
                    Gender=1,Bio="My name is small",
                    Birthday= new DateTime(1998,7,15)
                },
            };
            //su dung using system.linq truy xuat du lieu 1 doi tuong trong danh sach theo id
            Account account= accounts.FirstOrDefault(ac => ac.Id== id);
            // gửi đối tượng account qua view 
            ViewBag.account = account;
            return View();
        }
        
    }
}
