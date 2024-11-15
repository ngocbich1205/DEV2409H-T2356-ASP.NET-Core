using System.ComponentModel.DataAnnotations.Schema;

namespace lab06_code_first.Models
{
    [Table("Categories")]
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
