using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    [Table("orderItems")]
    public class OrderItemEntitie : BaseEntitie
    {
        public long productId { get; set; }
        [StringLength(255, ErrorMessage = "Name length must be less 256")]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("Order")]
        public long orderId { get; set; }
        [InverseProperty("OrderItems")]
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        [ForeignKey("productId")]
        public Product Product { get; set; }
    }
}
