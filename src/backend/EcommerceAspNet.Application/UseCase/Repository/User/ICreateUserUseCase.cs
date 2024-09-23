using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repositorie.User
{
    public interface ICreateUserUseCase
    {
        public Task<ResponseCreateUser> Execute(RequestCreateUser request);
    }
}
