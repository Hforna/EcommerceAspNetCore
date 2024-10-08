using EcommerceAspNet.Application.UseCase.Repository.Order;
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
    public class UpdateQuantityUseCase : IUpdateQuantityUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetUserByToken _userByToken;
        private readonly IOrderWriteOnlyRepository _orderWriteRepository;
        private readonly IOrderReadOnlyRepository _orderReadRepository;
        private readonly IProductReadOnlyRepository _productReadOnly;

        public UpdateQuantityUseCase(IUnitOfWork unitOfWork, IGetUserByToken userByToken, IOrderWriteOnlyRepository orderWriteRepository, IOrderReadOnlyRepository orderReadRepository, IProductReadOnlyRepository productReadOnly)
        {
            _unitOfWork = unitOfWork;
            _userByToken = userByToken;
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _productReadOnly = productReadOnly;
        }

        public async Task Execute(long id, int quantity)
        {
            var user = await _userByToken.GetUser();
            var orderItem = await _orderReadRepository.OrderItemByIdAndUser(user, id);

            if (orderItem is null)
                throw new ProductException("Product isn't on order");

            var product = await _productReadOnly.ProductById(orderItem.productId);

            var order = await _orderReadRepository.UserOrder(user);
            
            var unitPriceString = (product.Price * quantity).ToString("F2");
            var unitPrice = float.Parse(unitPriceString);

            orderItem.Quantity = quantity;
            orderItem.UnitPrice = unitPrice;

            order.TotalPrice += unitPrice;

            _orderWriteRepository.UpdateOrder(order);

            _orderWriteRepository.UpdateOrderItem(orderItem);
            await _unitOfWork.Commit();
        }
    }
}
