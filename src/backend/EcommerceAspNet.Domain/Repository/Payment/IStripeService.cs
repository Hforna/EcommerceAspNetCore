using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Payment
{
    public interface IStripeService
    {
        public Task<Session> GoToCheckout(UserEntitie user, IList<OrderItemEntitie> orderItems);
    }
}
