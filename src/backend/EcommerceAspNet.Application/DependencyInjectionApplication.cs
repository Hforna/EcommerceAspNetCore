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

namespace EcommerceAspNet.Application
{
    public static class DependencyInjectionApplication
    {
        public static void AddApplication(this IServiceCollection services)
        { 
            AddRepositories(services);
            AddAutoMapper(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IPasswordCryptography, PasswordCryptrography>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<IGetProfileUseCase, GetProfileUseCase>();
            services.AddScoped<IRequestDeleteAccount, RequestDeleteAccountUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            services.AddScoped<IUpdateImageProductUseCase, UpdateImageProductUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(d =>
            
                new AutoMapper.MapperConfiguration(d => { d.AddProfile(new RequestToEntitie()); }).CreateMapper()
            );
        }
    }
}
