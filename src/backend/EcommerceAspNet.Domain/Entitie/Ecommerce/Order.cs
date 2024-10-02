using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    public class Order : BaseEntitie
    {
        public long UserId { get; set; }
        public float TotalPrice { get; set; }
        public IList<OrderItemEntitie?> OrderItems { get; set; }
    }
}
