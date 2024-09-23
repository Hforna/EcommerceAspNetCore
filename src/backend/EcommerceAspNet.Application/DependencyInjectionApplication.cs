﻿using EcommerceAspNet.Application.UseCase.Repositorie.User;
using EcommerceAspNet.Application.Service.AutoMapper;
using EcommerceAspNet.Application.UseCase.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application
{
    public static class DependencyInjectionApplication
    {
        public static void AddApplication(this IServiceCollection services)
        { 
            AddRepositories(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(d =>
            
                new AutoMapper.MapperConfiguration(d => { d.AddProfile(new RequestToEntitie()); }).CreateMapper()
            );
        }
    }
}
