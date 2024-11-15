using System;
using System.Collections.Generic;

namespace lab06.Models​;

public partial class Pnhap
{
    public string SoPn { get; set; } = null!;

    public DateTime? NgayNhap { get; set; }

    public string SoDh { get; set; } = null!;

    public virtual Dondh SoDhNavigation { get; set; } = null!;
}
