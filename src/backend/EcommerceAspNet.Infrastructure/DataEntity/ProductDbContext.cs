using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Repository.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.DataEntity
{
    public class ProductDbContext : IProductReadOnlyRepository, IProductWriteOnlyRepository
    {
        private readonly ProjectDbContext _dbContext;

        public ProductDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public async Task<ProductEntitie?> ProductById(long id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(ProductEntitie product)
        {
            _dbContext.Products.Update(product);
        }
    }
}
