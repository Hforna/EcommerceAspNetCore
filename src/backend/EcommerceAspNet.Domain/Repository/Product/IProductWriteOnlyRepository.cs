﻿using EcommerceAspNet.Domain.Entitie.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Product
{
    public interface IProductWriteOnlyRepository
    {
        public void Update(ProductEntitie product);

        public void Delete(ProductEntitie product);
        public void Add(ProductEntitie product);
    }
}
