using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Login
{
    public interface ILoginGoogleUseCase
    {
        public Task<string> Execute(string name, string email);
    }
}
