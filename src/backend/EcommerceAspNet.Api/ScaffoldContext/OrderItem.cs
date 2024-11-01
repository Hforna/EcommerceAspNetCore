using System;
using System.Collections.Generic;

namespace EcommerceAspNet.Api.ScaffoldContext;

public partial class OrderItem
{
    public long Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool Active { get; set; }

    public long ProductId { get; set; }

    public long OrderId { get; set; }

    public int Quantity { get; set; }

    public float UnitPrice { get; set; }

    public string Name { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
