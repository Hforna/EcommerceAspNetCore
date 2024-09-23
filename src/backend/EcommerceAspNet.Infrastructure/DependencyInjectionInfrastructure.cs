using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Infrastructure.DataEntity;
using EcommerceAspNet.Infrastructure.Security.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static void AddInstrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services, configuration);
        }

        private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var signKey = configuration.GetValue<string>("settings:token:signKey");
            var minutesExpire = configuration.GetValue<long>("settings:token:minutesExpire");

            services.AddScoped<IUnitOfWork, UserDbContext>();
            services.AddScoped<IUserReadOnlyRepository, UserDbContext>();
            services.AddScoped<IAddUser, UserDbContext>();
            services.AddScoped<IGenerateToken>(t => new GenerateToken(signKey!, minutesExpire));
        }
    }
}
