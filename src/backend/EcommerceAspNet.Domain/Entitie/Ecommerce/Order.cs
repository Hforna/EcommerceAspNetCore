using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    [Table("orders")]
    public class Order : BaseEntitie
    {
        [ForeignKey("Users")]
        public long UserId { get; set; }
        public float TotalPrice { get; set; }
        public IList<OrderItemEntitie?> OrderItems { get; set; }
    }
}
