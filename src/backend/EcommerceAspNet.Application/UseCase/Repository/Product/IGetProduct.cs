using EcommerceAspNet.Communication.Response.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Product
{
    public interface IGetProduct
    {
        public Task<ResponseProductFull> Execute(long id);
    }
}
