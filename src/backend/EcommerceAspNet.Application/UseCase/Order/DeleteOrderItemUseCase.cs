﻿using EcommerceAspNet.Application.UseCase.Repository.Order;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Order
{
    public class DeleteOrderItemUseCase : IDeleteOrderItemUseCase
    {
        private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetUserByToken _userByToken;

        public DeleteOrderItemUseCase(IOrderWriteOnlyRepository orderWriteOnlyRepository, IOrderReadOnlyRepository orderReadOnlyRepository, IUnitOfWork unitOfWork, IGetUserByToken userByToken)
        {
            _orderWriteOnlyRepository = orderWriteOnlyRepository;
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _unitOfWork = unitOfWork;
            _userByToken = userByToken;
        }

        public async Task Execute(long id)
        {
            var user = await _userByToken.GetUser();

            var orderItem = await _orderReadOnlyRepository.OrderItemByIdAndUser(user, id);

            if (orderItem is null)
                throw new ProductException("Order item doesn't exists");

            var order = await _orderReadOnlyRepository.UserOrder(user);

            order.TotalPrice -= orderItem.UnitPrice;
            _orderWriteOnlyRepository.UpdateOrder(order);           

            _orderWriteOnlyRepository.DeleteOrderItem(orderItem);
            await _unitOfWork.Commit();
        }
    }
}
