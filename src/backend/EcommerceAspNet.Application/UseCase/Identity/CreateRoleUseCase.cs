using EcommerceAspNet.Application.UseCase.Repository.Identity;
using EcommerceAspNet.Domain.Entitie.Identity;
using EcommerceAspNet.Exception.Exception;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Identity
{
    public class CreateRoleUseCase : ICreateRoleUseCase
    {
        private readonly RoleManager<RoleEntitie> _roleManager;

        public CreateRoleUseCase(RoleManager<RoleEntitie> roleManager) => _roleManager = roleManager;

        public async Task Execute(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ResponseErrorJson("Name cannot be null");

            if (await _roleManager.RoleExistsAsync(name))
                throw new ResponseErrorJson("Role already exists");

            await _roleManager.CreateAsync(new RoleEntitie(name));

        }
    }
}
