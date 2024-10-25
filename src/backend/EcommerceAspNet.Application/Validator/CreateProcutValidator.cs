using EcommerceAspNet.Communication.Request.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Validator
{
    public class CreateProcutValidator : AbstractValidator<RequestCreateProduct>
    {
        public CreateProcutValidator()
        {

        }
    }
}
