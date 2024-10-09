using EcommerceAspNet.Domain.Entitie.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Coupon
{
    public interface ICouponReadOnlyRepository
    {
        public Task<bool?> couponExists(string name);
        public Task<DiscountCouponEntitie?> Get(string name);
    }
}
