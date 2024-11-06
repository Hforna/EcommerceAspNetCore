using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.Order;
using EcommerceAspNet.Communication.Response.Order;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Exception.Exception;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Order
{
    public class GetOrderUseCase : IGetOrderUseCase
    {
        private readonly IGetUserByToken _userByToken;
        private readonly IOrderReadOnlyRepository _orderReadOnly;
        private readonly IMapper _mapper;
        private readonly IProductReadOnlyRepository _productReadOnly;
        private readonly SqidsEncoder<long> _sqidsEncoder;

        public GetOrderUseCase(IGetUserByToken userByToken, IOrderReadOnlyRepository orderReadOnly, 
            IMapper mapper, IProductReadOnlyRepository productReadOnly,
            SqidsEncoder<long> sqidsEncoder)
        {
            _userByToken = userByToken;
            _orderReadOnly = orderReadOnly;
            _mapper = mapper;
            _productReadOnly = productReadOnly;
            _sqidsEncoder = sqidsEncoder;
        }

        public async Task<ResponseUserOrder> Execute()
        {
            var user = await _userByToken.GetUser();

            var order = await _orderReadOnly.UserOrder(user);

            if (order is null)
                throw new OrderException("Order not found");

            var orderItems = await _orderReadOnly.OrderItemsProduct(order);

            var response = new ResponseUserOrder() { TotalPrice = order.TotalPrice};

            var dictCategorys = new Dictionary<(string, long), List<EcommerceAspNet.Domain.Entitie.Ecommerce.OrderItemEntitie>>();

            foreach(var orderItem in orderItems)
            {
                var category = await _productReadOnly.CategoryById(orderItem.Product.CategoryId);
                if(dictCategorys.ContainsKey((category.Name, category.Id)))
                {
                    dictCategorys[(category.Name, category.Id)].Add(orderItem);
                } else
                {
                    dictCategorys[(category.Name, category.Id)] = new List<Domain.Entitie.Ecommerce.OrderItemEntitie>() { orderItem };
                }
            }

            var responseOrderItem = _mapper.Map<IList<ResponseCategoryProduct>>(dictCategorys.Select(d => new ResponseCategoryProduct()
            {
                CategoryId = _sqidsEncoder.Encode(d.Key.Item2),
                CategoryName = d.Key.Item1,
                Products = _mapper.Map<IList<ResponseOrderItem>>(d.Value)
            }).ToList());

            response.ProductsCategorys = responseOrderItem;
            
            return response;
        }
    }
}
