using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using EcommerceAspNet.Application.Service.Email;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.User;
using System.Text;

namespace EcommerceAspNet.Api.BackgroundServices
{
    public class SendSalesReportService : BackgroundService
    {
        private IServiceProvider _serviceProvider;
        private Timer _timer;

        public SendSalesReportService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(GenerateAndSendReport, null, TimeSpan.Zero, TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }

        private async void GenerateAndSendReport(object state)
        {
            if (state is CancellationToken token && token.IsCancellationRequested)
                return;

            var scope = _serviceProvider.CreateScope();

            var _userReadOnly = scope.ServiceProvider.GetRequiredService<IUserReadOnlyRepository>();
            var _productReadOnly = scope.ServiceProvider.GetRequiredService<IProductReadOnlyRepository>();
            var _emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

            var users = await _userReadOnly.GetAdmins();

            if (users is not null)
            {
                var products = _productReadOnly.GetBestProducts(7);

                if (products.Count != 0)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Sales report of the week");
                    sb.AppendLine("Products list:");

                    foreach (var product in products)
                    {
                        var priceSold = product.Value * product.Key.Price;
                        sb.AppendLine($"Product name: {product.Key}, quantity sold: {product.Value}, price sold: {priceSold}");
                    }

                    foreach (var user in users)
                    {
                        var name = user.UserName;
                        var email = user.Email;

                        await _emailService.SendEmail(sb.ToString(), email, name);
                    }
                }
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(stoppingToken);
        }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
