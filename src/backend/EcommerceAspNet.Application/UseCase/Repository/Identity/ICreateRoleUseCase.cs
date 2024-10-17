using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Identity
{
    public interface ICreateRoleUseCase
    {
        public Task Execute(string name);
    }
}
