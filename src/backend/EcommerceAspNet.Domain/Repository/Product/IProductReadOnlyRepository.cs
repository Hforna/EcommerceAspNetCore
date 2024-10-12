using EcommerceAspNet.Domain.Entitie.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Product
{
    public interface IProductReadOnlyRepository
    {
        public Task<ProductEntitie?> ProductById(long id);

        public Task<IList<ProductEntitie>?> GetProducts(long? id = null, int? price = null);

        public Task<bool> CategoryExists(long? id);

        public Task<ProductEntitie?> GetProductByUid(Guid uid);
    }
}
