using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Enum;
using EcommerceAspNet.Domain.Repository.Product;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace EcommerceAspNet.Infrastructure.DataEntity
{
    public class ProductDbContext : IProductReadOnlyRepository, IProductWriteOnlyRepository
    {
        private readonly ProjectDbContext _dbContext;

        public ProductDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public void Add(ProductEntitie product)
        {
            _dbContext.Products.Add(product);
        }

        public async Task<bool> CategoryExists(long? id)
        {
            return await _dbContext.Categories.AnyAsync(d => d.Id == id && d.Active);
        }

        public void Delete(ProductEntitie product)
        {            
            _dbContext.Products.Remove(product);
        }

        public Dictionary<ProductEntitie, int> GetBestProducts(int days)
        {
            var orders = _dbContext.OrderItems.Where(d => d.Active == false && d.CreatedOn.AddDays(7).Day == DateTime.Now.Day);

            var products = new Dictionary<ProductEntitie, int>();

            foreach(var order in orders)
            {
                if (products.ContainsKey(order.Product))
                {
                    products[order.Product]++;
                } else
                {
                    products[order.Product] = 1;
                }
            }

            return (Dictionary<ProductEntitie, int>)products;
        }

        public async Task<ProductEntitie?> GetProductByUid(Guid uid)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(d => d.ProductIdentifier == uid && d.Active);
        }

        public PagedList.IPagedList<ProductEntitie> GetProducts(long? id, int? price, int numberPage = 1)
        {
            var products = _dbContext.Products.Where(d => d.Active);

            if(id is not null && price is null)
                products = products.Where(d => d.Active && d.CategoryId == id);

            if(price is not null && id is null)
                products = products.Where(d => d.Active == true && d.groupPrice == (PriceEnum)price!);

            return products.ToPagedList(numberPage, 4);
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
