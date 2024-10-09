using EcommerceAspNet.Application.Service.Email;
using EcommerceAspNet.Application.UseCase.Repository.Payment;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Payment
{
    public class StripeWebhookUseCase : IStripeWebhookUseCase
    {
        private readonly string _secretKey;
        private readonly EmailService _emailService;

        public StripeWebhookUseCase(string secretKey, EmailService emailService)
        {
            _secretKey = secretKey;
            _emailService = emailService;
        }

        public async Task Execute(string jsonBody, string stripeSignature)
        {
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(jsonBody, stripeSignature, _secretKey, throwOnApiVersionMismatch: false);

                switch (stripeEvent.Type)
                {
                    case "payment_intent.succeeded":
                        var checkoutService = stripeEvent.Data.Object as PaymentIntent;
                        var customerId = checkoutService.CustomerId;
                        var typeCoin = checkoutService.Currency;
                        var amountTotal = checkoutService.Amount / 100;

                        var customerService = new CustomerService();
                        var customer = await customerService.GetAsync(customerId);
                        var customerEmail = customer.Email;
                        var customerName = customer.Name;

                        var message = $"Hello {customerName}, your purchase totaled {amountTotal} {typeCoin}";
                        await _emailService.SendEmail(message, customerName, customerEmail);
                        break;

                    default:
                        break;
                }
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe error: {ex.Message}");
                throw; 
            }
            catch (System.Exception ex)
            {                
                Console.WriteLine($"General error: {ex.Message}");
                throw; 
            }
        }
    }
}
