using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security.Cryptography;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Exception.Exception;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.User
{
    public class VerifyCodePasswordUseCase : IVerifyCodePassword
    {
        private readonly UserManager<Domain.Entitie.User.User> _userManager;
        private readonly IUserReadOnlyRepository _userReadOnly;

        public VerifyCodePasswordUseCase(UserManager<Domain.Entitie.User.User> userManager, IUserReadOnlyRepository userReadOnly)
        {
            _userManager = userManager;
            _userReadOnly = userReadOnly;
        }

        public async Task<ResponseUserResetPassword> Execute(RequestVerifyCodePassword request)
        {
            var user = await _userReadOnly.UserByEmail(request.Email);
            
            if (user == null)
            {
                throw new UserException("E-mail doesn't exists");
            }

            var codeVerify = _userManager.VerifyTwoFactorTokenAsync(user, "Email", request.Code);

            if(await codeVerify == false)
                throw new UserException("Password token invalid");

            var tokenPassword = await _userManager.GenerateUserTokenAsync(user, "Email", "ResetPassword");

            return new ResponseUserResetPassword() { Token = tokenPassword };
        }
    }
}
