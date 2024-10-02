using EcommerceAspNet.Application.UseCase.Repositorie.User;
using EcommerceAspNet.Application.Service.AutoMapper;
using EcommerceAspNet.Application.UseCase.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Application.Service.Cryptography;
using EcommerceAspNet.Application.UseCase.Repository.Login;
using EcommerceAspNet.Application.UseCase.Login;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Application.UseCase.Product;
using Microsoft.Extensions.Configuration;
using Sqids;
using EcommerceAspNet.Application.UseCase.Repository.Order;
using EcommerceAspNet.Application.UseCase.Order;

namespace EcommerceAspNet.Application
{
    public static class DependencyInjectionApplication
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        { 
            AddRepositories(services);
            AddAutoMapper(services);
            AddSqids(services, configuration);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IPasswordCryptography, PasswordCryptrography>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<IGetProfileUseCase, GetProfileUseCase>();
            services.AddScoped<IRequestDeleteAccount, RequestDeleteAccountUseCase>();
            services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
            services.AddScoped<IUpdateImageProductUseCase, UpdateImageProductUseCase>();
            services.AddScoped<IRequestDeleteProduct, RequestDeleteProductUseCase>();
            services.AddScoped<ILoginGoogleUseCase, LoginGoogleUseCase>();
            services.AddScoped<IGetProducts, GetProductsUseCase>();
            services.AddScoped<IAddOrderUseCase, AddOrderUseCase>();
        }

        private static void AddSqids(IServiceCollection services, IConfiguration configuration)
        {
            var alphabet = configuration.GetValue<string>("settings:sqIds:Alphabet")!;
            var digits = configuration.GetValue<int>("settings:sqIds:Digits");

            var sqids = new SqidsEncoder<long>(new SqidsOptions()
            {
                Alphabet = alphabet,
                MinLength = digits,
            });

            services.AddSingleton(sqids);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(d =>
            
                new AutoMapper.MapperConfiguration(d => { d.AddProfile(new Mapper()); }).CreateMapper()
            );
        }
    }
}
