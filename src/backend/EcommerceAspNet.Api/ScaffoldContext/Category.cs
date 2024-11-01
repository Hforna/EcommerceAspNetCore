using System;
using System.Collections.Generic;

namespace EcommerceAspNet.Api.ScaffoldContext;

public partial class Category
{
    public long Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool Active { get; set; }

    public string Name { get; set; } = null!;
}
