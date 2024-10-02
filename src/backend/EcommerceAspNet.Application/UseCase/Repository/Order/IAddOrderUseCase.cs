using EcommerceAspNet.Communication.Response.Order;
using EcommerceAspNet.Communication.Response.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Order
{
    public interface IAddOrderUseCase
    {
        public Task<ResponseUserOrder> Execute(long id);
    }
}
