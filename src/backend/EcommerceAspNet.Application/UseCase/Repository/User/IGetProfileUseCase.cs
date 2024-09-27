using EcommerceAspNet.Communication.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.User
{
    public interface IGetProfileUseCase
    {
        public Task<ResponseGetProfile> Execute();
    }
}
