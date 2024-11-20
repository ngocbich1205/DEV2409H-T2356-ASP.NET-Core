using System.ComponentModel.DataAnnotations.Schema;

namespace lab07.Areas.Admin.Models
{
    [Table("Blogs")]
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image {  get; set; }
    }
}
