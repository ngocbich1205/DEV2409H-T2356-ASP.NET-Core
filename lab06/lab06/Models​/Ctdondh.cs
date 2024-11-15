using System;
using System.Collections.Generic;

namespace lab06.Models​;

public partial class Ctdondh
{
    public string SoDh { get; set; } = null!;

    public string MaVtu { get; set; } = null!;

    public int SlDat { get; set; }

    public virtual Vattu MaVtuNavigation { get; set; } = null!;
}
