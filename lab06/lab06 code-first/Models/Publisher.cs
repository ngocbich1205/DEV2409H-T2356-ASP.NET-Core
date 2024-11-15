using System.ComponentModel.DataAnnotations.Schema;

namespace lab06_code_first.Models
{
    [Table("Publishers")]
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public ICollection<Book> Books { get; set; }

    }
}
