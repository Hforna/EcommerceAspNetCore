﻿using EcommerceAspNet.Domain.Entitie.Ecommerce;
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

        public async Task<IList<Order>> OrdersNotActive(User? user = null)
        {
            if(user is null)
                return await _dbContext.Orders.Include(d => d.OrderItems).AsNoTracking().Where(o => o.Active == false).ToListAsync();

            return await _dbContext.Orders.Include(d => d.OrderItems).AsNoTracking().Where(o => o.UserId == user.Id && o.Active == false).ToListAsync();
        }

        public async Task<Order?> OrderById(long id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(d => d.Id == id && d.Active);
        }

        public async Task<OrderItemEntitie?> OrderItemByIdAndUser(User user, long id)
        {
            var order = await _dbContext.Orders.AsNoTracking().FirstOrDefaultAsync(d => d.UserId == user.Id && d.Active);

            return await _dbContext.OrderItems.FirstOrDefaultAsync(d => d.Id == id && d.orderId == order.Id && order.Active);
        }

        public async Task<OrderItemEntitie?> OrderItemExists(Order order, long id)
        {
            return await _dbContext.OrderItems.FirstOrDefaultAsync(d => d.productId == id && d.orderId == order.Id && d.Active);
        }

        public async Task<Order?> OrderItemList(Order order)
        {
            var orderUser = _dbContext.Orders.Include(d => d.OrderItems).FirstOrDefaultAsync(f => f == order && f.Active);

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

        public async Task<Order?> UserOrder(User user)
        {
            return await _dbContext.Orders.Include(d => d.OrderItems).FirstOrDefaultAsync(d => d.UserId == user.Id && d.Active);
        }

        public void UpdateOrderItemList(List<OrderItemEntitie> orderItemList)
        {
            _dbContext.OrderItems.UpdateRange(orderItemList);
        }

        public async Task<List<OrderItemEntitie>> OrderItemsProduct(Order order)
        {
            return await _dbContext.OrderItems.Include(d => d.Product).Where(d => d.orderId == order.Id).ToListAsync();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.Where(d => d.Active && d.CreatedOn.AddDays(7) >= DateTime.UtcNow).ToListAsync();
        }
    }
}
