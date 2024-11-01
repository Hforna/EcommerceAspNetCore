using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Exception.Exception;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.User
{
    public class ConfirmEmailUseCase : IConfirmEmail
    {
        private readonly UserManager<Domain.Entitie.User.User> _userManager;
        private readonly IUserReadOnlyRepository _userReadOnly;

        public ConfirmEmailUseCase(UserManager<Domain.Entitie.User.User> userManager, IUserReadOnlyRepository userReadOnly)
        {
            _userManager = userManager;
            _userReadOnly = userReadOnly;
        }

        public async Task Execute(string email, string token)
        {
            var user = await _userReadOnly.UserByEmail(email);

            if (user is null)
                throw new UserException("User doesn't exists");

            await _userManager.ConfirmEmailAsync(user, token);
        }
    }
}
