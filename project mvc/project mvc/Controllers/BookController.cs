using Microsoft.AspNetCore.Mvc;
using project_mvc.Models;

namespace project_mvc.Controllers
{
    public class BookController : Controller
    {
        protected Book book = new Book();
        public IActionResult Index()
        {
            //danh sách genres convert selectlistitem để hiển thị combobox
            ViewBag.authors = book.Authors;//truyen du lieu selectlistitem qua view
            ViewBag.genres = book.Genres;
            var books= book.GetBookList();

            return View(books);//truyen du lieu qua view duoi dang tham so
        }
        public IActionResult Create()
        {
            ViewBag.authors=book.Authors;
            ViewBag.genres = book.Genres;
            Book model = new Book();
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.authors=book.Authors;
            ViewBag.Genres = book.Genres;
            Book model = new Book();
            return View(model);
        }
        public PartialViewResult PopularBook()
        {
            var books= book.GetBookList();
            return PartialView(books);
        }
    }
}
