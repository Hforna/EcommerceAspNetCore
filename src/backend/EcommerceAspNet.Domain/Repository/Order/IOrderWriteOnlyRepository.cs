using EcommerceAspNet.Domain.Entitie.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Order
{
    public interface IOrderWriteOnlyRepository
    {
        public void AddOrder(Entitie.Ecommerce.Order order);
        public void UpdateOrder(Entitie.Ecommerce.Order order);
        public void AddOrderItem(OrderItemEntitie orderItem);
        public void UpdateOrderItem(OrderItemEntitie orderItem);
        public void DeleteOrderItem(OrderItemEntitie orderItem);
    }
}
