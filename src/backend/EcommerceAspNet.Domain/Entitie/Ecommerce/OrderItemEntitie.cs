using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    public class OrderItemEntitie : BaseEntitie
    {
        [ForeignKey("products")]
        public long productId { get; set; }
        [StringLength(255, ErrorMessage = "Name length must be less 256")]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("orders")]
        public long orderId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public Product Product { get; set; }
    }
}
