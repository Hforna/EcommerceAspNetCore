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
using EcommerceAspNet.Application.UseCase.Repository.Login;
using EcommerceAspNet.Application.UseCase.Login;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Application.UseCase.Product;
using Microsoft.Extensions.Configuration;
using Sqids;
using EcommerceAspNet.Application.UseCase.Repository.Order;
using EcommerceAspNet.Application.UseCase.Order;
using EcommerceAspNet.Application.UseCase.Repository.Payment;
using EcommerceAspNet.Application.UseCase.Payment;
using EcommerceAspNet.Application.UseCase.Repository.Comment;
using EcommerceAspNet.Application.UseCase.Comment;
using EcommerceAspNet.Application.Service.Email;
using EcommerceAspNet.Application.UseCase.Repository.Identity;
using EcommerceAspNet.Application.UseCase.Identity;
using Microsoft.AspNetCore.Identity;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Application.Validator;

namespace EcommerceAspNet.Application
{
    public static class DependencyInjectionApplication
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        { 
            AddRepositories(services);
            AddAutoMapper(services);
            AddSqids(services, configuration);
            AddEmail(services, configuration);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<IGetProfileUseCase, GetProfileUseCase>();
            services.AddScoped<IRequestDeleteAccount, RequestDeleteAccountUseCase>();
            services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
            services.AddScoped<IUpdateImageProductUseCase, UpdateImageProductUseCase>();
            services.AddScoped<IRequestDeleteProduct, RequestDeleteProductUseCase>();
            services.AddScoped<ILoginGoogleUseCase, LoginGoogleUseCase>();
            services.AddScoped<IGetProducts, GetProductsUseCase>();
            services.AddScoped<IAddOrderUseCase, AddOrderUseCase>();
            services.AddScoped<IStripeUseCase, StripeUseCase>();
            services.AddScoped<ICreateComment, CreateCommentUseCase>();
            services.AddScoped<IUpdateImageUser, UpdateImageUserUseCase>();
            services.AddScoped<IGetProduct, GetProductUseCase>();
            services.AddScoped<IDeleteOrderItemUseCase, DeleteOrderItemUseCase>();
            services.AddScoped<IGetOrderUseCase, GetOrderUseCase>();
            services.AddScoped<IUpdateQuantityUseCase, UpdateQuantityUseCase>();
            services.AddScoped<IStripeWebhookUseCase, StripeWebhookUseCase>();
            services.AddScoped<IGetHistoryOrders, GetHistoryOrderUseCase>();
            services.AddScoped<ICreateRoleUseCase, CreateRoleUseCase>();
            services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
            services.AddScoped<IVerifyCodePassword, VerifyCodePasswordUseCase>();
            services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
            services.AddSingleton(new PasswordValidator<User>());
            services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
            services.AddScoped<IConfirmEmail, ConfirmEmailUseCase>();
            services.AddScoped<IProductDuoMostBougth, ProductDuoMostBougthUseCase>();
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

        private static void AddEmail(IServiceCollection services, IConfiguration configuration)
        {
            var email = configuration.GetValue<string>("settings:email:email");
            var name = configuration.GetValue<string>("settings:email:name");
            var password = configuration.GetValue<string>("settings:email:password");

            services.AddSingleton(new EmailService(password, email, name));
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(d =>
                new AutoMapper.MapperConfiguration(s =>
                {
                    var sqids = d.GetService<SqidsEncoder<long>>()!;
                    s.AddProfile(new Mapper(sqids));
                }).CreateMapper()
            );
        }
    }
}
