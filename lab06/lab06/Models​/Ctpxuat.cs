using System;
using System.Collections.Generic;

namespace lab06.Models​;

public partial class Ctpxuat
{
    public string SoPx { get; set; } = null!;

    public string MaVtu { get; set; } = null!;

    public int SlXuat { get; set; }

    public decimal DgXuat { get; set; }

    public virtual Vattu MaVtuNavigation { get; set; } = null!;
}
