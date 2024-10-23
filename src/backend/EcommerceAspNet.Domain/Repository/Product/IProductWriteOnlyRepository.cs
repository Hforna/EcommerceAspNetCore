using EcommerceAspNet.Domain.Entitie.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Product
{
    public interface IProductWriteOnlyRepository
    {
        public void Update(Entitie.Ecommerce.Product product);

        public void Delete(Entitie.Ecommerce.Product product);
        public void Add(Entitie.Ecommerce.Product product);
    }
}
