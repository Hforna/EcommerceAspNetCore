using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Domain.Entitie.User;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Validator
{
    public class ResetPasswordValidator : AbstractValidator<RequestResetPassword>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password.Length).GreaterThanOrEqualTo(8).WithMessage("Password must have 8 or more digits");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Message can't be null");
        }
    }
}
