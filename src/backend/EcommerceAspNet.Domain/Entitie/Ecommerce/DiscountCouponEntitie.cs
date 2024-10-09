using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    public class DiscountCouponEntitie : BaseEntitie
    {
        public string name { get; set; }
        public float valueDiscount { get; set; }
        public DateTime validateData { get; set; }
    }
}
