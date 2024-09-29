using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.User
{
    public interface IDeleteUserUseCase
    {
        public Task Execute(Guid uid);
    }
}
