using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    [Table("coupons")]
    public class DiscountCouponEntitie : BaseEntitie
    {
        public string name { get; set; }
        public float valueDiscount { get; set; }
        public DateTime validateData { get; set; }
    }
}
