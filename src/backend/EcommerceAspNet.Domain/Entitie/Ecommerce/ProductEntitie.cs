using EcommerceAspNet.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    public class ProductEntitie : BaseEntitie
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public long Stock { get; set; }
        public string? ImageIdentifier { get; set; }
        public long CategoryId { get; set; }
        public PriceEnum groupPrice { get; set; } = 0;
        public Guid ProductIdentifier { get; set; } = Guid.NewGuid();
    }
}
