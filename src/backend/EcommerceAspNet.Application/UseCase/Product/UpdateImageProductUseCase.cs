using EcommerceAspNet.Application.Extension;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.Storage;
using EcommerceAspNet.Exception.Exception;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Product
{
    public class UpdateImageProductUseCase : IUpdateImageProductUseCase
    {
        private readonly IProductWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductReadOnlyRepository _repositoryRead;
        private readonly IAzureStorageService _storage;

        public UpdateImageProductUseCase(IProductReadOnlyRepository productRead, IUnitOfWork unitOfWork, IProductWriteOnlyRepository writeOnlyRepository, IAzureStorageService storage)
        {
            _repositoryRead = productRead;
            _unitOfWork = unitOfWork;
            _repositoryWrite = writeOnlyRepository;
            _storage = storage;
        }

        public async Task Execute(IFormFile file, long id)
        {
            var product = await _repositoryRead.ProductById(id);

            if (product is null)
                throw new ProductException("Product doesn't exist");

            var fileStream = file.OpenReadStream();

            var fileValidate = FileImageExtension.ValidateImage(fileStream);

            if(fileValidate.isImage == false)
                throw new ProductException("File format wrong");

            product.ImageIdentifier = $"{Guid.NewGuid()}{fileValidate.typeImage}";

            _repositoryWrite.Update(product);

            await _unitOfWork.Commit();

            await _storage.Upload(product!, fileStream, product.ImageIdentifier);
        }
    }
}
