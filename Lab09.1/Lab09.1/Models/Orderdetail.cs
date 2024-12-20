using System;
using System.Collections.Generic;

namespace Lab09._1.Models;

public partial class Orderdetail
{
    public int Id { get; set; }

    public long? Idord { get; set; }

    public long? Idproduct { get; set; }

    public decimal? Price { get; set; }

    public int? Qty { get; set; }

    public decimal? Total { get; set; }

    public int? ReturnQty { get; set; }

    public virtual Customer IdNavigation { get; set; } = null!;
}
