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
    }
}
