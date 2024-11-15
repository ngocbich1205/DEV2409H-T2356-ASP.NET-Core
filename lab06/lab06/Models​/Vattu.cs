using System;
using System.Collections.Generic;

namespace lab06.Models​;

public partial class Vattu
{
    public string MaVtu { get; set; } = null!;

    public string TenVtu { get; set; } = null!;

    public string? DvTinh { get; set; }

    public float PhanTram { get; set; }
}
