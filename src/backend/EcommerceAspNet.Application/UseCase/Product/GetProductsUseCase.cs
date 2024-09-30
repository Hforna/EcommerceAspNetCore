using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Repository.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Product
{
    public class GetProductsUseCase : IGetProducts
    {
        private readonly IProductReadOnlyRepository _repository;

        public Task<ResponseProductsJson> Execute()
        {
            return new ResponseProductsJson()
            {
                Products = 
            }
        }
    }
}
