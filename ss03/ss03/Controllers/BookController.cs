using Microsoft.AspNetCore.Mvc;
using ss03.Models;

namespace ss03.Controllers
{
    public class BookController : Controller
    {
        protected Book book = new Book();
        public IActionResult Index()
        {
            //danh sách genres convert selectlistitem để hiển thị trên combobox 
            ViewBag.authors = book.Authors;
            ViewBag.genres = book.Genres;
            var books = book.GetBookList();
            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.authors= book.Authors; // truyền dữ liệu selectlistitem qua view 
            ViewBag.genres = book.Genres; // truyền dữ liệu selectlistitem qua view
            Book model = new Book();
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.authors = book.Authors; // truyền dữ liệu selectlistitem qua view 
            ViewBag.genres = book.Genres; // truyền dữ liệu selectlistitem qua view
            Book model = book.GetBookById(id); //lấy dữ liệu một cuốn sách theo id
            return View(model);
        }
        public PartialViewResult PopularBook()
        {
            var books = book.GetBookList();
            return PartialView(books);
        }
    }
}
