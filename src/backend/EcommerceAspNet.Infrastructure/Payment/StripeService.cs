using Azure;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository.Payment;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Storage;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Payment
{
    public class StripeService : IStripeService
    {
        private readonly IProductReadOnlyRepository _repositoryProductRead;
        private readonly IAzureStorageService _storageService;

        public StripeService(IProductReadOnlyRepository repositoryProductRead, IAzureStorageService storageService)
        {
            _repositoryProductRead = repositoryProductRead;
            _storageService = storageService;
        }

        public async Task<Session> GoToCheckout(UserEntitie user, Order order)
        {
            var domain = "http://localhost:5008";

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = $"{domain}/swagger.html",
                CancelUrl = $"{domain}/swagger.html",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                CustomerEmail = user.Email
            };

            foreach(var item in order.OrderItems)
            {
                var product = await _repositoryProductRead.ProductById(item!.productId);
                var imageUrl = await _storageService.GetUrlImage(product!, product!.ImageIdentifier!);
                var imagesUrl = new List<string>();

                imagesUrl.Add(imageUrl);

                var sessionListItem = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "brl",
                        UnitAmount = (long)(product.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Images = imagesUrl
                        }
                    },
                    Quantity = item.Quantity
                };

                options.LineItems.Add(sessionListItem);
            }

            var sessionService = new SessionService();
            var session = sessionService.Create(options);

            return session;
        }
    }
}
