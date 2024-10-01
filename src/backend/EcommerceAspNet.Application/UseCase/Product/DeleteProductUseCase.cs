using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.ServiceBus;
using EcommerceAspNet.Domain.Repository.Storage;
using EcommerceAspNet.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Product
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductReadOnlyRepository _repositoryRead;
        private readonly IProductWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureStorageService _storageService;

        public DeleteProductUseCase(IProductReadOnlyRepository repositoryRead, IProductWriteOnlyRepository repositoryWrite, 
            IUnitOfWork unitOfWork, IAzureStorageService storageService)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
            _storageService = storageService;
        }

        public async Task Execute(Guid uid)
        {
            var product = await _repositoryRead.GetProductByUid(uid);

            if (product is null)
                throw new ProductException("Product doesn't exists");

            await _storageService.DeleteContainer(product);

            _repositoryWrite.Delete(product);
            await _unitOfWork.Commit();
        }
    }
}
