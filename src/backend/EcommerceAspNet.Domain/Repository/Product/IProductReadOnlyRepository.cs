using EcommerceAspNet.Domain.Entitie.Ecommerce;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Product
{
    public interface IProductReadOnlyRepository
    {
        public Task<Entitie.Ecommerce.Product?> ProductById(long id);

        public IPagedList<Entitie.Ecommerce.Product> GetProducts(long? id = null, int? price = null, int numberPage = 1);

        public Task<bool> CategoryExists(long? id);

        public Task<Entitie.Ecommerce.Product?> GetProductByUid(Guid uid);

        public Dictionary<Entitie.Ecommerce.Product, int> GetBestProducts(int days);

        public Task<CategoryEntitie?> CategoryById(long id);
    }
}
