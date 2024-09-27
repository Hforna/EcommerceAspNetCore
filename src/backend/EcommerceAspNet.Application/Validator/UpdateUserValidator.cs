using EcommerceAspNet.Communication.Request.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Validator
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(r => r.Email).EmailAddress().WithMessage("E-mail format is wrong");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(r => r.NewPassword).MinimumLength(8).WithMessage("New password must have 8 or more digits");
        }
    }
}
