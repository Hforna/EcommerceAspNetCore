using EcommerceAspNet.Application.UseCase.Repository.Payment;
using EcommerceAspNet.Communication.Request.Payment;
using EcommerceAspNet.Communication.Response.Payment;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Repository.Coupon;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Payment;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Exception.Exception;
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
        private readonly ICouponReadOnlyRepository _couponReady;

        public StripeUseCase(IGetUserByToken userLogged, IOrderReadOnlyRepository repositoryOrderRead, 
            IStripeService stripeService, ICouponReadOnlyRepository couponReadOnly)
        {
            _userLogged = userLogged;
            _repositoryOrderRead = repositoryOrderRead;
            _stripeService = stripeService;
            _couponReady = couponReadOnly;
        }

        public async Task<ResponseUrlStripe> Execute(RequestDiscountCoupon request)
        {
            if (await _couponReady.couponExists(request.DiscountCoupon) == false)
                throw new CouponException("Coupon doesn't exists");

            var user = await _userLogged.GetUser();
            var order = await _repositoryOrderRead.UserOrder(user);

            if (order is null)
                throw new ProductException("You don't have order yet");

            var orderItem = await _repositoryOrderRead.OrderItemList(order!);
            var orderItemList = (List<OrderItemEntitie>)order.OrderItems;

            if(string.IsNullOrEmpty(request.DiscountCoupon) == false)
            {
                var coupon = await _couponReady.Get(request.DiscountCoupon);

                var valueDiscount = 5;

                orderItemList = orderItemList.Select(item =>
                {
                    item.UnitPrice -= (item.UnitPrice / 100 * valueDiscount);

                    return item;
                }).ToList();
            }

            var service = await _stripeService.GoToCheckout(user!, orderItemList!);

            return new ResponseUrlStripe() { Url = service.Url };
        }
    }
}
