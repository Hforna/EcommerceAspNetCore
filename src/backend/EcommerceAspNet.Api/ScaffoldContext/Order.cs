using System;
using System.Collections.Generic;

namespace EcommerceAspNet.Api.ScaffoldContext;

public partial class Order
{
    public long Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool Active { get; set; }

    public long UserId { get; set; }

    public float TotalPrice { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
