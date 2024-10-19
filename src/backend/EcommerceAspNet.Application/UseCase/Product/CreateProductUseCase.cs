using AutoMapper;
using EcommerceAspNet.Application.Extension;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Communication.Request.Product;
using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Storage;
using EcommerceAspNet.Exception.Exception;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Product
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IProductWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureStorageService _azureStorage;
        private readonly IMapper _mapper;
        private readonly SqidsEncoder<long> _sqidsEncoder;

        public CreateProductUseCase(IProductWriteOnlyRepository repositoryWrite, IUnitOfWork unitOfWork, IAzureStorageService azureStorage, IMapper mapper, SqidsEncoder<long> sqidsEncoder)
        {
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
            _azureStorage = azureStorage;
            _mapper = mapper;
            _sqidsEncoder = sqidsEncoder;
        }

        public async Task<ResponseProductShort> Execute(RequestCreateProduct request)
        {
            var stream = request.ProductImage.OpenReadStream();

            var (isImage, imageType) = FileImageExtension.ValidateImage(stream);

            if (isImage == false)
                throw new ProductException("File must be a image");

            var product = _mapper.Map<ProductEntitie>(request);

            product.ImageIdentifier = $"{Guid.NewGuid()}{imageType}";

            _repositoryWrite.Add(product);
            await _unitOfWork.Commit();

            await _azureStorage.Upload(product, stream, product.ImageIdentifier);

            var response = _mapper.Map<ResponseProductShort>(product);

            response.ImageUrl = await _azureStorage.GetUrlImageProduct(product, product.ImageIdentifier);
            response.Id = _sqidsEncoder.Encode(product.Id);

            return response;
        }
    }
}
