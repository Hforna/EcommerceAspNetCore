using EcommerceAspNet.Communication.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Order
{
    public interface IGetOrderUseCase
    {
        public Task<ResponseUserOrder> Execute();
    }
}
