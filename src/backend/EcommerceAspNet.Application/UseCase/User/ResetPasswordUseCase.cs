using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Application.Validator;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security.Cryptography;
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
    public class ResetPasswordUseCase : IResetPasswordUseCase
    {
        private readonly UserManager<Domain.Entitie.User.User> _userManager;
        private readonly IUserReadOnlyRepository _userReadOnly;
        private readonly IUserWriteOnlyRepository _userWriteOnly;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordCryptography _passwordCryptography;

        public ResetPasswordUseCase(UserManager<Domain.Entitie.User.User> userManager, IUserReadOnlyRepository userReadOnly, 
            IUserWriteOnlyRepository userWriteOnly, IUnitOfWork unitOfWork, 
            IPasswordCryptography passwordCryptography)
        {
            _userManager = userManager;
            _userReadOnly = userReadOnly;
            _userWriteOnly = userWriteOnly;
            _unitOfWork = unitOfWork;
            _passwordCryptography = passwordCryptography;
        }

        public async Task<ResponseResetPassword> Execute(RequestResetPassword request, string email, string token)
        {
            var user = await _userReadOnly.UserByEmail(email);

            if (user is null)
                throw new UserException("E-mail doesn't exists");

            await Validate(request, user);

            var tokenValidate = await _userManager.VerifyUserTokenAsync(user, "Email", "ResetPassword", token);

            if (tokenValidate == false)
                throw new UserException("Token is invalid");

            user.Password = _passwordCryptography.Encrypt(request.Password);

            _userWriteOnly.Update(user);
            await _unitOfWork.Commit();

            return new ResponseResetPassword() { Text = $"Congratulations {user.UserName} you change your password" };
        }

        public async Task Validate(RequestResetPassword request, Domain.Entitie.User.User user)
        {
            var validator = new ResetPasswordValidator();
            var result = validator.Validate(request);

            var password = _passwordCryptography.Encrypt(request.Password);

            if (await _userReadOnly.PasswordEqual(user, password))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure() { ErrorMessage = "Password can't be the same" });

            if (request.Password != request.RepeatPassword)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure() { ErrorMessage = "Passwords are not the same" });

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
                throw new UserException(errorMessages);
            }
        }
    }
}
