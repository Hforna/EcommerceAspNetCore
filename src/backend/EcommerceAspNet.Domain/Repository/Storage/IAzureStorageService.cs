﻿using EcommerceAspNet.Domain.Entitie.Ecommerce;
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
        public Task Upload(Entitie.Ecommerce.Product product, Stream file, string fileName);

        public Task UploadUser(Entitie.User.User user, Stream file, string fileName);

        public Task DeleteContainer(Entitie.Ecommerce.Product product);
        public Task DeleteUserContainer(Entitie.User.User user);

        public Task<string> GetUrlImageProduct(Entitie.Ecommerce.Product product, string fileName);

        public Task<string> GetUrlImageUser(Entitie.User.User user, string fileName);
    }
}
