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

        public void Delete(ProductEntitie product)
        {            
            _dbContext.Products.Remove(product);
        }

        public async Task<ProductEntitie?> GetProductByUid(Guid uid)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(d => d.ProductIdentifier == uid && d.Active);
        }

        public async Task<IList<ProductEntitie>?> GetProducts()
        {
            return await _dbContext.Products.Where(d => d.Active == true).ToListAsync();
        }

        public async Task<ProductEntitie?> ProductById(long id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id && p.Active);
        }

        public void Update(ProductEntitie product)
        {
            _dbContext.Products.Update(product);
        }
    }
}
