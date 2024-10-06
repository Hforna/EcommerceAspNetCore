using EcommerceAspNet.Communication.Request.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Validator
{
    public class CreateUserValidator : AbstractValidator<RequestCreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(8);
            When(u => string.IsNullOrEmpty(u.Email) == false, () =>
            {
                RuleFor(u => u.Email).EmailAddress();
            });
        }
    }
}
