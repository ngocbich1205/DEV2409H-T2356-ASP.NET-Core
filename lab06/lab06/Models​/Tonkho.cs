using System;
using System.Collections.Generic;

namespace lab06.Models​;

public partial class Tonkho
{
    public string NamThang { get; set; } = null!;

    public string MaVtu { get; set; } = null!;

    public int Sldau { get; set; }

    public int TongSln { get; set; }

    public int TonSlx { get; set; }

    public int? Slcuoi { get; set; }

    public virtual Vattu MaVtuNavigation { get; set; } = null!;
}
