using EcommerceAspNet.Communication.Request.Comment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Validator
{
    public class CreateCommentValidator : AbstractValidator<RequestCreateComment>
    {
        public CreateCommentValidator()
        {
            RuleFor(d => d.Text).MaximumLength(2000).WithMessage("Text length must be less");
            RuleFor(d => d.Note).LessThanOrEqualTo(5).WithMessage("Note have only 5 avaliatons");
        }
    }
}
