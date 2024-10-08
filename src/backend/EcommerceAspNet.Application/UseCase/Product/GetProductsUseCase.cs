﻿using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Storage;
using EcommerceAspNet.Exception.Exception;
using Org.BouncyCastle.Security.Certificates;
using Sqids;
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
        private readonly IAzureStorageService _storageService;
        private readonly IMapper _mapper;
        private readonly SqidsEncoder<long> _sqids;

        public GetProductsUseCase(IProductReadOnlyRepository repository, IAzureStorageService storageService, IMapper mapper, SqidsEncoder<long>sqids)
        {
            _repository = repository;
            _storageService = storageService;
            _mapper = mapper;
            _sqids = sqids;
        }

        public async Task<ResponseProductsJson> Execute(long? id)
        {
            if (id is not null && await _repository.CategoryExists(id) == false)
                throw new ProductException("This category doesn't exists");

            var products = await _repository.GetProducts(id);

            var responses = products!.Select(async product =>
            {
                var response = _mapper.Map<ResponseProductShort>(product);

                response.ImageUrl = await _storageService.GetUrlImageProduct(product, product.ImageIdentifier!);
                response.Id = _sqids.Encode(product.Id);
                response.CategoryId = _sqids.Encode(product.CategoryId);

                return response;
            });

            var responseTask = await Task.WhenAll(responses);

            return new ResponseProductsJson()
            {
                Products = responseTask
            };
        }
    }
}
