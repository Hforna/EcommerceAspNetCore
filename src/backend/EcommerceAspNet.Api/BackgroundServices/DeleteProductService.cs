
using Azure.Messaging.ServiceBus;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Infrastructure.ServiceBus;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Runtime.CompilerServices;

namespace EcommerceAspNet.Api.BackgroundServices
{
    public class DeleteProductService : BackgroundService
    {
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _serviceProvider;

        public DeleteProductService(DeleteProductProcessor processor, IServiceProvider serviceProvider)
        {
            _processor = processor.GetProcessor();
            _serviceProvider = serviceProvider;            
        }
            
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += ProcessMessageAsync;

            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync(stoppingToken);
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();

            var scope = _serviceProvider.CreateScope();
            var useCase = scope.ServiceProvider.GetRequiredService<IDeleteProductUseCase>();

            var productIdentifier = Guid.Parse(message);

            await useCase.Execute(productIdentifier);
        }

        private async Task ProcessErrorAsync(ProcessErrorEventArgs args) => await Task.CompletedTask;

        ~DeleteProductService() => Dispose();

        public override void Dispose()
        {
            base.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
