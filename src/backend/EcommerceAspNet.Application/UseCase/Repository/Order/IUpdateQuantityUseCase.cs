using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Order
{
    public interface IUpdateQuantityUseCase
    {
        public Task Execute(long id, int quantity);
    }
}
