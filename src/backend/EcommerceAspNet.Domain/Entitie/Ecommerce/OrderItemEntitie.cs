using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    public class OrderItemEntitie : BaseEntitie
    {
        public long productId { get; set; }
        public string Name { get; set; } = string.Empty;
        public long orderId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public ProductEntitie Product { get; set; }
    }
}
