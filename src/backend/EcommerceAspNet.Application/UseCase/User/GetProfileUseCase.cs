using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Communication.Response.Order;
using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
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

namespace EcommerceAspNet.Application.UseCase.User
{
    public class GetProfileUseCase : IGetProfileUseCase
    {
        private readonly IGetUserByToken _userByToken;
        private readonly IMapper _mapper;
        private readonly SqidsEncoder<long> _sqidsEncoder;
        private readonly IProductReadOnlyRepository _productReadOnly;
        private readonly IOrderReadOnlyRepository _orderReadOnly;

        public GetProfileUseCase(IGetUserByToken userByToken, IMapper mapper, 
            SqidsEncoder<long> sqidsEncoder, IProductReadOnlyRepository productReadOnly, IOrderReadOnlyRepository orderReadOnly)
        {
            _userByToken = userByToken;
            _mapper = mapper;
            _sqidsEncoder = sqidsEncoder;
            _productReadOnly = productReadOnly;
            _orderReadOnly = orderReadOnly;
        }

        public async Task<ResponseGetProfile> Execute()
        {
            var user = await _userByToken.GetUser() ?? throw new UserException("User doesn't exists");

            var orderItems = await _orderReadOnly.OrderItemsProduct(user.UserOrder);

            var response = new ResponseGetProfile() { UserOrder = _mapper.Map<ResponseUserOrder>(user.UserOrder), Email = user.Email };
            var dictCategory = new Dictionary<long, List<OrderItemEntitie>>();

            foreach (var item in orderItems)
            {
                if (dictCategory.ContainsKey(item.Product.CategoryId))
                {
                    dictCategory[item.Product.CategoryId].Add(item);
                } else
                {
                    dictCategory[item.Product.CategoryId] = new List<OrderItemEntitie>() { item };
                }
            }

            var categorysTask = dictCategory.Select(async d => {
                var category = await _productReadOnly.CategoryById(d.Key);
                var response = new ResponseCategoryProduct()
                {

                    CategoryId = _sqidsEncoder.Encode(d.Key),
                    CategoryName = category.Name,
                    Products = _mapper.Map<IList<ResponseOrderItem>>(d.Value),
                };
                return response;
                });

            var categoryProducts = await Task.WhenAll(categorysTask);

            response.UserOrder.ProductsCategorys = categoryProducts.ToList();

            return response;
        }
    }
}
