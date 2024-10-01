using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Product
{
    public interface IRequestDeleteProduct
    {
        public Task Execute(long id);
    }
}
