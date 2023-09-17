using System;
using System.Collections.Generic;

namespace Odev1_Northwnd_Project.Models;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
