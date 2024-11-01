using EcommerceAspNet.Application.Service.Email;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Domain.Entitie.User;
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
    public class ForgotPasswordUseCase : IForgotPasswordUseCase
    {
        private readonly EmailService _emailService;
        private readonly UserManager<Domain.Entitie.User.User> _userManager;
        private readonly IUserReadOnlyRepository _userReadOnly;

        public ForgotPasswordUseCase(EmailService emailService, UserManager<Domain.Entitie.User.User> userManager, IUserReadOnlyRepository userReadOnly)
        {
            _emailService = emailService;
            _userManager = userManager;
            _userReadOnly = userReadOnly;
        }

        public async Task Execute(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new UserException("Invalid e-mail");

            var user = await _userReadOnly.LoginByEmail(email);

            if (user is null)
                return;

           var code =  await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            var message = $"Your password code is: {code}";

            await _emailService.SendEmail(message, email);
        }
    }
}
