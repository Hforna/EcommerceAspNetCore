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
        public long orderId { get; set; }
        public string Quantity { get; set; } = string.Empty;
        public float UnitPrice { get; set; }
    }
}
