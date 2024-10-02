using EcommerceAspNet.Application.UseCase.Repository.Payment;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Payment;
using EcommerceAspNet.Domain.Repository.Security;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Payment
{
    public class StripeUseCase : IStripeUseCase
    {
        private readonly IGetUserByToken _userLogged;
        private readonly IOrderReadOnlyRepository _repositoryOrderRead;
        private readonly IStripeService _stripeService;

        public StripeUseCase(IGetUserByToken userLogged, IOrderReadOnlyRepository repositoryOrderRead, IStripeService stripeService)
        {
            _userLogged = userLogged;
            _repositoryOrderRead = repositoryOrderRead;
            _stripeService = stripeService;
        }

        public async Task<Session> Execute()
        {
            var user = await _userLogged.GetUser();
            var order = await _repositoryOrderRead.UserOrder(user);
            var orderItemList = await _repositoryOrderRead.OrderItemList(order!);

            return await _stripeService.GoToCheckout(user!, orderItemList!);
        }
    }
}
