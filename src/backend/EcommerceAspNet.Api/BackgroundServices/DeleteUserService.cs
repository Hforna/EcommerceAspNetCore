
using Azure.Messaging.ServiceBus;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Infrastructure.ServiceBus;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Runtime.CompilerServices;

namespace EcommerceAspNet.Api.BackgroundServices
{
    public class DeleteUserService : BackgroundService
    {
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _serviceProvider;

        public DeleteUserService(DeleteUserProcessor processor, IServiceProvider serviceProvider)
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
            var useCase = scope.ServiceProvider.GetRequiredService<IDeleteUserUseCase>();

            var userIdentifier = Guid.Parse(message);

            await useCase.Execute(userIdentifier);
        }

        private async Task ProcessErrorAsync(ProcessErrorEventArgs args) => await Task.CompletedTask;

        ~DeleteUserService() => Dispose();

        public override void Dispose()
        {
            base.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
