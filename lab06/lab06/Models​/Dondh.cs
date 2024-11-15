using System;
using System.Collections.Generic;

namespace lab06.Models​;

public partial class Dondh
{
    public string SoDh { get; set; } = null!;

    public DateTime NgayDh { get; set; }

    public string MaNhaCc { get; set; } = null!;

    public virtual Nhacc MaNhaCcNavigation { get; set; } = null!;

    public virtual ICollection<Pnhap> Pnhaps { get; set; } = new List<Pnhap>();
}
