using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Enum;
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

        public async Task<bool> CategoryExists(long? id)
        {
            return await _dbContext.Categories.AnyAsync(d => d.Id == id && d.Active);
        }

        public void Delete(ProductEntitie product)
        {            
            _dbContext.Products.Remove(product);
        }

        public async Task<ProductEntitie?> GetProductByUid(Guid uid)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(d => d.ProductIdentifier == uid && d.Active);
        }

        public async Task<IList<ProductEntitie>?> GetProducts(long? id = null, int? price = null, int numberPage = 1)
        {
            var products = _dbContext.Products;

            if (id is null && price is null)
                return await products.Where(d => d.Active).ToListAsync();

            if(id is not null && price is null)
                return await products.Where(d => d.Active && d.Id == id).ToListAsync();

            if(price is not null && id is null)
                return await products.Where(d => d.Active && d.groupPrice == (PriceEnum)price!).ToListAsync();

            return await products.Where(d => d.Active && d.CategoryId == id && d.groupPrice == (PriceEnum)price!).ToListAsync();
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
