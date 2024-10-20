using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.Order;
using EcommerceAspNet.Communication.Response.Order;
using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Order
{
    public class AddOrderUseCase : IAddOrderUseCase
    {
        private readonly IProductReadOnlyRepository _repositoryProductRead;
        private readonly IOrderReadOnlyRepository _repositoryOrderRead;
        private readonly IOrderWriteOnlyRepository _repositoryOrderWrite;
        private readonly IGetUserByToken _userLogged;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddOrderUseCase(IProductReadOnlyRepository repositoryProductRead, IOrderReadOnlyRepository repositoryOrderRead, 
            IOrderWriteOnlyRepository repositoryOrderWrite, 
            IGetUserByToken userLogged, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repositoryProductRead = repositoryProductRead;
            _repositoryOrderRead = repositoryOrderRead;
            _repositoryOrderWrite = repositoryOrderWrite;
            _userLogged = userLogged;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseUserOrder> Execute(long id)
        {
            var product = await _repositoryProductRead.ProductById(id);

            if (product is null)
                throw new ProductException("Product doesn't exists");

            if (product.Stock < 1)
                throw new ProductException("Product is out of stock");

            var user = await _userLogged.GetUser();
            var orderUser = await _repositoryOrderRead.UserOrder(user!);

            if (orderUser is null)
            {
                orderUser = new Domain.Entitie.Ecommerce.Order() { UserId = user!.Id };
                _repositoryOrderWrite.AddOrder(orderUser);
                await _unitOfWork.Commit();
            }

            var orderItem = await _repositoryOrderRead.OrderItemExists(orderUser, id);
            if(orderItem is null)
            {
                var orderItems = new OrderItemEntitie()
                {
                    orderId = orderUser!.Id,
                    productId = product.Id,
                    Quantity = 1,
                    UnitPrice = product.Price,
                    Name = product.Name
                };

                _repositoryOrderWrite.AddOrderItem(orderItems);
            } else
            {
                orderItem.Quantity += 1;
                orderItem.UnitPrice = product.Price * orderItem.Quantity;

                _repositoryOrderWrite.UpdateOrderItem(orderItem);
            }
            await _unitOfWork.Commit();

            var orderList = await _repositoryOrderRead.OrderItemList(orderUser);

            float totalPrice = 0;

            foreach(var item in orderList!.OrderItems)
            {
                totalPrice += item!.UnitPrice;
            }

            orderList.TotalPrice = totalPrice;

            _repositoryOrderWrite.UpdateOrder(orderList);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseUserOrder>(orderList);
        }
    }
}
