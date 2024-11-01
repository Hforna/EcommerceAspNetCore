using System;
using System.Collections.Generic;

namespace EcommerceAspNet.Api.ScaffoldContext;

public partial class Comment
{
    public long Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool Active { get; set; }

    public string Text { get; set; } = null!;

    public short? Note { get; set; }

    public long ProductId { get; set; }

    public long? UserId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual AspNetUser? User { get; set; }
}
