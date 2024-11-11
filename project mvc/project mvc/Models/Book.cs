using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace project_mvc.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
     
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image {  get; set; }
        public float Price { get; set; }
        public int TotalPage { get; set; }
        public string Sumary { get; set; }
        public List<Book> GetBookList()
        {
            List<Book> books = new List<Book>()
            {
                new Book() {
                    Id = 1,
                    Title="chi pheo",
                    AuthorId = 1,
                    GenreId = 1,
                    Image="images/product/sach1.jpg",
                    Price=50000,
                    Sumary="",
                    TotalPage=250
                },
                 new Book() {
                    Id = 2,
                    Title="lao hac",
                    AuthorId = 1,
                    GenreId = 1,
                    Image="images/product/sach2.jpg",
                    Price=50000,
                    Sumary="",
                    TotalPage=250
                },
                  new Book() {
                    Id = 3,
                    Title="chi pheo",
                    AuthorId = 1,
                    GenreId = 1,
                    Image="images/product/sach3.jpg",
                    Price=50000,
                    Sumary="",
                    TotalPage=250
                }
            };
            return books;
        }
        //chi tiet 1 cuon sach theo id( nho using system.linq)
        public Book GetBookById(int id)
        {
            Book book= this.GetBookList().FirstOrDefault(b => b.Id == id);
            return book;
        }
        public List<SelectListItem> Authors { get; } = new List<SelectListItem>()
        {
            new SelectListItem {Value="1", Text="Nam Cao"},
            new SelectListItem {Value="2", Text="Ngô Tất Tố"},
            new SelectListItem {Value="3", Text="Adamkhom"}
        };
        public List<SelectListItem> Genres { get; }= new List<SelectListItem>()
        { 
            new SelectListItem {Value="1", Text="Truyện tranh"},
            new SelectListItem {Value="2", Text="Văn học đương đại"},
            new SelectListItem {Value="3", Text="Truyện cười"}
        };
    }
   
}
