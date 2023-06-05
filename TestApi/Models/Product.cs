using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? Size { get; set; }

    public string? Varienty { get; set; }

    public double? Price { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
}
