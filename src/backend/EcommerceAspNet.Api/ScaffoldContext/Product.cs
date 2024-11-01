using System;
using System.Collections.Generic;

namespace EcommerceAspNet.Api.ScaffoldContext;

public partial class Product
{
    public long Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool Active { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public float Price { get; set; }

    public long Stock { get; set; }

    public long CategoryId { get; set; }

    public string? ImageIdentifier { get; set; }

    public Guid ProductIdentifier { get; set; }

    public int? GroupPrice { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
