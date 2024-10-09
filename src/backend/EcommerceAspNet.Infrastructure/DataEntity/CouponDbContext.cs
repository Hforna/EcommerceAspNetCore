using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Repository.Coupon;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.DataEntity
{
    public class CouponDbContext : ICouponReadOnlyRepository
    {
        private readonly ProjectDbContext _dbContext;

        public CouponDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task<bool?> couponExists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return await _dbContext.Coupons.AnyAsync(c => c.name == name);
        }

        public async Task<DiscountCouponEntitie?> Get(string name)
        {
            return await _dbContext.Coupons.FirstOrDefaultAsync(d => d.name == name);
        }
    }
}
