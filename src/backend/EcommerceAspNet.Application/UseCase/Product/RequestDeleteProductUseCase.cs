using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.ServiceBus;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Product
{
    public class RequestDeleteProductUseCase : IRequestDeleteProduct
    {
        private readonly IProductReadOnlyRepository _repositoryRead;
        private readonly IProductWriteOnlyRepository _repositoryWrite;
        private readonly ISendDeleteProduct _sendMessage;
        private readonly IUnitOfWork _unitOfWork;

        public RequestDeleteProductUseCase(IProductReadOnlyRepository repositoryRead, IProductWriteOnlyRepository repositoryWrite, ISendDeleteProduct sendMessage, IUnitOfWork unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _sendMessage = sendMessage;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var product = await _repositoryRead.ProductById(id);

            if (product == null)
                throw new ProductException("Product doesn't exists");

            product.Active = false;

            _repositoryWrite.Update(product);
            await _unitOfWork.Commit();

            await _sendMessage.SendMessage(product);
        }
    }
}
