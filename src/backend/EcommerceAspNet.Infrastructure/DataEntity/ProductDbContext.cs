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

        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
        }

        public async Task<CategoryEntitie?> CategoryById(long id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CategoryExists(long? id)
        {
            return await _dbContext.Categories.AnyAsync(d => d.Id == id && d.Active);
        }

        public void Delete(Product product)
        {            
            _dbContext.Products.Remove(product);
        }

        public Dictionary<Product, int> GetBestProducts(int days)
        {
            var orders = _dbContext.OrderItems.Where(d => d.Active == false && d.CreatedOn.AddDays(7).Day == DateTime.Now.Day);

            var products = new Dictionary<Product, int>();

            foreach(var order in orders)
            {
                if (products.ContainsKey(order.Product))
                {
                    products[order.Product] += order.Quantity;
                } else
                {
                    products[order.Product] = order.Quantity;
                }
            }

            return products.OrderBy(d => d.Value).Take(10).ToDictionary(d => d.Key, d => d.Value);
        }

        public async Task<Product?> GetProductByUid(Guid uid)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(d => d.ProductIdentifier == uid && d.Active);
        }

        public PagedList.IPagedList<Product> GetProducts(long? id, int? price, int numberPage = 1)
        {
            var products = _dbContext.Products.Include(d => d.Comments).Where(d => d.Active);

            if(id is not null && price is null)
                products = products.Where(d => d.CategoryId == id);

            if(price is not null && id is null)
                products = products.Where(d => d.groupPrice == (PriceEnum)price!);

            return products.OrderBy(d => d.Comments.Count).ToPagedList(numberPage, 4);
        }

        public async Task<Product?> ProductById(long id)
        {
            return await _dbContext.Products.Include(d => d.Comments).FirstOrDefaultAsync(p => p.Id == id && p.Active);
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
        }
    }
}
