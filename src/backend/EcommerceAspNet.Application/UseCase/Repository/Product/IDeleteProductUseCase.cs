using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Product
{
    public interface IDeleteProductUseCase
    {
        public Task Execute(Guid uid);
    }
}
