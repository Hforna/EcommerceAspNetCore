using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Infrastructure.DataEntity;
using EcommerceAspNet.Infrastructure.Security.Token;
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
            services.AddScoped<IUserReadOnlyRepository, UserDbContext>();
            services.AddScoped<IUserWriteOnlyRepository, UserDbContext>();
            services.AddScoped<IGenerateToken>(t => new GenerateToken(signKey!, minutesExpire));
            services.AddScoped<IValidateToken>(t => new ValidateToken(signKey!));
            services.AddScoped<IGetUserByToken, GetUserByToken>();
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
    }
}
