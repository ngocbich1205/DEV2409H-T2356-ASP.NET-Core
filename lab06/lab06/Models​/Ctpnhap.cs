using System;
using System.Collections.Generic;

namespace lab06.Models​;

public partial class Ctpnhap
{
    public string SoPn { get; set; } = null!;

    public string MaVtu { get; set; } = null!;

    public int Slnhap { get; set; }

    public decimal Dgnhap { get; set; }

    public virtual Vattu MaVtuNavigation { get; set; } = null!;
}
