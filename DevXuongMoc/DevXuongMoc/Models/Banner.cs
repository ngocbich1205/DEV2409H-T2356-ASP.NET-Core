using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevXuongMoc.Models
{
    public partial class Banner
    {
        public int Id { get; set; }

        [Display(Name = "Ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Display(Name = "Phụ đề")]
        public string? SubTitle { get; set; }

        [Display(Name = "URL")]
        public string? Urls { get; set; }

        [Display(Name = "Thứ tự")]
        public int Orders { get; set; }

        [Display(Name = "Loại")]
        public string? Type { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Người tạo")]
        public string? AdminCreated { get; set; }

        [Display(Name = "Người cập nhật")]
        public string? AdminUpdated { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Notes { get; set; }

        [Display(Name = "Trạng thái")]
        public byte Status { get; set; }

        [Display(Name = "Đã xóa")]
        public bool Isdelete { get; set; }
    }
}
