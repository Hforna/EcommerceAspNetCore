using System;
using System.Collections.Generic;

namespace EcommerceAspNet.Api.ScaffoldContext;

public partial class AspNetUserToken
{
    public long UserId { get; set; }

    public string LoginProvider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
