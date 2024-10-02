using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Payment;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.ServiceBus;
using EcommerceAspNet.Domain.Repository.Storage;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Infrastructure.DataEntity;
using EcommerceAspNet.Infrastructure.Payment;
using EcommerceAspNet.Infrastructure.Security.Token;
using EcommerceAspNet.Infrastructure.ServiceBus;
using EcommerceAspNet.Infrastructure.Storage;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static void AddInstrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services, configuration);
            AddSqlServerConnection(services, configuration);
            FluentMsigrator(services, configuration);
            AddServiceBus(services, configuration);
            AddStorageBlob(services, configuration);
            AddPayment(services, configuration);
        }

        private static void AddSqlServerConnection(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sqlserverconnection");
            services.AddDbContext<ProjectDbContext>(d => d.UseSqlServer(connectionString));
        }

        private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var signKey = configuration.GetValue<string>("settings:token:signKey");
            var minutesExpire = configuration.GetValue<long>("settings:token:minutesExpire");

            services.AddScoped<IUnitOfWork, UserDbContext>();

            //User repositories
            services.AddScoped<IUserReadOnlyRepository, UserDbContext>();
            services.AddScoped<IUserWriteOnlyRepository, UserDbContext>();

            services.AddScoped<IGenerateToken>(t => new GenerateToken(signKey!, minutesExpire));
            services.AddScoped<IValidateToken>(t => new ValidateToken(signKey!));
            services.AddScoped<IGetUserByToken, GetUserByToken>();

            //Product repositories
            services.AddScoped<IProductReadOnlyRepository, ProductDbContext>();
            services.AddScoped<IProductWriteOnlyRepository, ProductDbContext>();

            //Order repositories
            services.AddScoped<IOrderReadOnlyRepository, OrderDbContext>();
            services.AddScoped<IOrderWriteOnlyRepository, OrderDbContext>();
        }

        private static void FluentMsigrator(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sqlserverconnection");
            services.AddFluentMigratorCore().ConfigureRunner(opt =>
            {
                opt.AddSqlServer().WithGlobalConnectionString(connectionString).ScanIn(Assembly.Load("EcommerceAspNet.Infrastructure")).For.All();
            });

            services.AddScoped<FluentMigrator.Runner.Processors.ProcessorOptions>();
        }

        private static void AddStorageBlob(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("settings:storageBlob:azure");
            var blobService = new BlobServiceClient(connectionString);

            services.AddScoped<IAzureStorageService>(opt => new AzureStorageService(blobService));
        }

        private static void AddPayment(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStripeService, StripeService>();
        }

        private static void AddServiceBus(IServiceCollection services, IConfiguration configuration)
        {
            var connectService = configuration.GetValue<string>("settings:serviceBus:azure");

            var client = new ServiceBusClient(connectService, new ServiceBusClientOptions() {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            });

            var sender = new SendProductUser(client.CreateSender("product"));
            services.AddScoped<ISendDeleteProduct>(opt => sender);

            var processor = new DeleteProductProcessor(client.CreateProcessor("product", new ServiceBusProcessorOptions()
            {
                MaxConcurrentCalls = 1
            }));

            services.AddSingleton(processor);
        }
    }
}
