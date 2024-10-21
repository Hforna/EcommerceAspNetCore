
using EcommerceAspNet.Application.Service.Email;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.User;
using System.Text;

namespace EcommerceAspNet.Api.BackgroundServices
{
    public class AbandonedCartService : BackgroundService
    {
        private readonly EmailService _emailService;
        private readonly IServiceProvider _serviceProvider;

        public AbandonedCartService(EmailService emailService, IServiceProvider serviceProvider)
        {
            _emailService = emailService;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = _serviceProvider.CreateScope();

            var orderReadOnly = scope.ServiceProvider.GetRequiredService<IOrderReadOnlyRepository>();
            var userReadOnly = scope.ServiceProvider.GetRequiredService<IUserReadOnlyRepository>();

            var orders = await orderReadOnly.GetAllOrders();

            if (orders.Count == 0)
                await Task.CompletedTask;

            foreach (var order in orders)
            {
                var user = await userReadOnly.UserById(order.UserId);

                var orderItems = await orderReadOnly.OrderItemsProduct(order);


                var sb = new StringBuilder();

                sb.AppendLine("don't forget complete your order");
                foreach (var item in orderItems)
                {
                    sb.AppendLine($"Product: {item.Name}, Price: {item.UnitPrice}");
                }

                var message = sb.ToString();

                await _emailService.SendEmail(message, user.Email, user.UserName);
            }
        }


        ~AbandonedCartService() => Dispose();

        public override void Dispose()
        {
            base.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
