using System;
using System.Collections.Generic;

namespace Odev1_Northwnd_Project.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
