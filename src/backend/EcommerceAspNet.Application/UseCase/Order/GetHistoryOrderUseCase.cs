using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.Order;
using EcommerceAspNet.Communication.Response.Order;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Order
{
    public class GetHistoryOrderUseCase : IGetHistoryOrders
    {
        private readonly IGetUserByToken _getUserByToken;
        private readonly IOrderReadOnlyRepository _orderReadOnly;
        private readonly IMapper _mapper;

        public GetHistoryOrderUseCase(IGetUserByToken getUserByToken, IOrderReadOnlyRepository orderReadOnly, IMapper mapper)
        {
            _getUserByToken = getUserByToken;
            _orderReadOnly = orderReadOnly;
            _mapper = mapper;
        }

        public async Task<IList<ResponseUserOrder>> Execute()
        {
            var user = await _getUserByToken.GetUser();

            var orders = await _orderReadOnly.OrdersNotActive(user);

            return _mapper.Map<IList<ResponseUserOrder>>(orders);
        }
    }
}
