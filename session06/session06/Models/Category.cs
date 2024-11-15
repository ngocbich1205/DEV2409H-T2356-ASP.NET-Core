using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace session06.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id {  get; set; }
        [Required(ErrorMessage ="Tên danh mục không được để trống")]
        [StringLength(100)]
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }
        [Column(TypeName ="tinyint")]
        public byte Status { get; set; }
        public DateTime CreatedDate { get; set; }
        //danh sách sản phẩm theo danh mục
        public ICollection<Product> Products { get; set; }
    }
}
