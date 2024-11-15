using System;
using System.Collections.Generic;

namespace lab06.Models​;

public partial class Nhacc
{
    public string MaNhaCc { get; set; } = null!;

    public string TenNhaCc { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string? DienThoai { get; set; }

    public virtual ICollection<Dondh> Dondhs { get; set; } = new List<Dondh>();
}
