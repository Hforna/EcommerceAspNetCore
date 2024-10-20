using Azure;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository.Payment;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Storage;
using Stripe;
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

        public async Task<Session> GoToCheckout(UserEntitie user, IList<OrderItemEntitie> orderItems)
        {
            var domain = "http://localhost:5008";

            var customerService = new CustomerService();
            var customer = await customerService.CreateAsync(new CustomerCreateOptions()
            {
                Email = user.Email,
                Name = user.UserName
            });

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = $"https://youtube.com",
                CancelUrl = $"https://youtube.com",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                Customer = customer.Id
            };

            foreach(var item in orderItems)
            {
                var product = await _repositoryProductRead.ProductById(item!.productId);
                var imageUrl = await _storageService.GetUrlImageProduct(product!, product!.ImageIdentifier!);
                var imagesUrl = new List<string>();

                if(string.IsNullOrEmpty(imageUrl) == false)
                    imagesUrl.Add(imageUrl);

                var ProductData = new SessionLineItemPriceDataProductDataOptions()
                {
                    Name = product.Name,
                    Description = product.Description,
                };

                if (imagesUrl.Count > 0)
                    ProductData.Images = imagesUrl;

                var sessionListItem = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "brl",
                        UnitAmount = (long)(item.UnitPrice / item.Quantity * 100),
                        ProductData = ProductData

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
