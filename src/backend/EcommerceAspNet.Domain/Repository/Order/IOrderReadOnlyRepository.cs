using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Order
{
    public interface IOrderReadOnlyRepository
    {
        public Task<Entitie.Ecommerce.Order?> OrderById(long id);

        public Task<Entitie.Ecommerce.Order?> UserOrder(UserEntitie user);

        public Task<OrderItemEntitie?> OrderItemExists(Entitie.Ecommerce.Order order, long id);
        public Task<Entitie.Ecommerce.Order?> OrderItemList(Entitie.Ecommerce.Order order);
    }
}
