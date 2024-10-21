using EcommerceAspNet.Application.Service.Email;
using EcommerceAspNet.Application.UseCase.Repository.Payment;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.User;
using Microsoft.Extensions.Configuration;
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
        private readonly IOrderReadOnlyRepository _orderReadOnly;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IOrderWriteOnlyRepository _orderWriteOnly;
        private readonly IUnitOfWork _unitOfWork;

        public StripeWebhookUseCase(IConfiguration configuration, EmailService emailService, 
            IOrderWriteOnlyRepository orderWriteOnly, IUserReadOnlyRepository userReadOnlyRepository, 
            IOrderReadOnlyRepository orderReadOnlyRepository, IUnitOfWork unitOfWork)
        {
            _secretKey = configuration.GetValue<string>("settings:stripe:webhookKey")!;
            _emailService = emailService;
            _orderWriteOnly = orderWriteOnly;
            _userReadOnlyRepository = userReadOnlyRepository;
            _orderReadOnly = orderReadOnlyRepository;
            _unitOfWork = unitOfWork;
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

                        var user = await _userReadOnlyRepository.UserByEmail(customerEmail);
                        var orderUser = await _orderReadOnly.UserOrder(user);

                        var orderItems = await _orderReadOnly.OrderItemsProduct(orderUser);

                        var orderUserList = orderItems.Select(item =>
                        {
                            item.Product.Stock -= item.Quantity;

                            return item;
                        }).ToList();

                        _orderWriteOnly.UpdateOrderItemList(orderUserList);

                        orderUser.Active = false;

                        _orderWriteOnly.UpdateOrder(orderUser);
                        await _unitOfWork.Commit();

                        var message = $"Hello {customerName}, your purchase totaled {amountTotal} {typeCoin}";
                        await _emailService.SendEmail(message, customerEmail, customerName);
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
