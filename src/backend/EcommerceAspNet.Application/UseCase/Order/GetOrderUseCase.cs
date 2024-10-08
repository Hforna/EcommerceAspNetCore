using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.Order;
using EcommerceAspNet.Communication.Response.Order;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Security;
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

        public GetOrderUseCase(IGetUserByToken userByToken, IOrderReadOnlyRepository orderReadOnly, IMapper mapper)
        {
            _userByToken = userByToken;
            _orderReadOnly = orderReadOnly;
            _mapper = mapper;
        }

        public async Task<ResponseUserOrder> Execute()
        {
            var user = await _userByToken.GetUser();

            var order = await _orderReadOnly.UserOrder(user);

            var response = _mapper.Map<ResponseUserOrder>(order);

            return response;
        }
    }
}
