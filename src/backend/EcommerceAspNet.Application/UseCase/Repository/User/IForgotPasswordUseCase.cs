using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.User
{
    public interface IForgotPasswordUseCase
    {
        public Task Execute(string email);
    }
}
