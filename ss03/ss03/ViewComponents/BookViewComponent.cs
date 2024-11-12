using Microsoft.AspNetCore.Mvc;
using ss03.Models;

namespace ss03.ViewComponents
{
    public class BookViewComponent: ViewComponent
    {
        protected Book Book= new Book();
        public IViewComponentResult Invoke()
        {
            var books = Book.GetBookList();
            return View(books);
        }
    }
}
