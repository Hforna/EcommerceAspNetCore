using EcommerceAspNet.Communication.Request.Payment;
using EcommerceAspNet.Communication.Response.Payment;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Payment
{
    public interface IStripeUseCase
    {
        public Task<ResponseUrlStripe> Execute(RequestDiscountCoupon request);
    }
}
