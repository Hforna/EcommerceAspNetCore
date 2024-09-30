using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Storage
{
    public interface IAzureStorageService
    {
        public Task Upload(ProductEntitie product, Stream file, string fileName);

        public Task DeleteContainer(UserEntitie user);

        public Task<string> GetUrlImage(string fileName);
    }
}
