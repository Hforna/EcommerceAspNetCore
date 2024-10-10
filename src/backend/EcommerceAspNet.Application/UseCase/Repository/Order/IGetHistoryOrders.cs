using AutoMapper;
using EcommerceAspNet.Communication.Response.Order;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Order
{
    public interface IGetHistoryOrders
    {
        public Task<IList<ResponseUserOrder>> Execute();
    }
}
