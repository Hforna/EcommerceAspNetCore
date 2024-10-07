using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.DataEntity
{
    public class OrderDbContext : IOrderReadOnlyRepository, IOrderWriteOnlyRepository
    {
        private readonly ProjectDbContext _dbContext;

        public OrderDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public void AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
        }

        public void AddOrderItem(OrderItemEntitie orderItem)
        {
            _dbContext.OrderItems.Add(orderItem);
        }

        public void DeleteOrderItem(OrderItemEntitie orderItem)
        {
            _dbContext.OrderItems.Remove(orderItem);
        }

        public async Task<Order?> OrderById(long id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(d => d.Id == id && d.Active);
        }

        public async Task<OrderItemEntitie?> OrderItemByIdAndUser(UserEntitie user, long id)
        {
            var order = await _dbContext.Orders.AsNoTracking().FirstOrDefaultAsync(d => d.UserId == user.Id);

            return await _dbContext.OrderItems.FirstOrDefaultAsync(d => d.Id == id && d.orderId == order.Id && order.Active);
        }

        public async Task<OrderItemEntitie?> OrderItemExists(Order order, long id)
        {
            return await _dbContext.OrderItems.FirstOrDefaultAsync(d => d.productId == id && d.orderId == order.Id && d.Active);
        }

        public async Task<Order?> OrderItemList(Order order)
        {
            var orderUser = _dbContext.Orders.Include(d => d.OrderItems).FirstOrDefaultAsync(f => f == order);

            return await orderUser;
        }

        public void UpdateOrder(Order order)
        {
            _dbContext.Orders.Update(order);
        }

        public void UpdateOrderItem(OrderItemEntitie orderItem)
        {
            _dbContext.OrderItems.Update(orderItem);
        }

        public async Task<Order?> UserOrder(UserEntitie user)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(d => d.UserId == user.Id);
        }
    }
}
