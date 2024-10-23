using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Communication.Response.Comment;
using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Repository.Comment;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Storage;
using EcommerceAspNet.Domain.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Product
{
    public class GetProductUseCase : IGetProduct
    {
        private readonly IProductReadOnlyRepository _repositoryProductRead;
        private readonly ICommentReadOnlyRepository _repositoryCommentRead;
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _storageService;
        private readonly IUserReadOnlyRepository _repositoryUserRead;

        public GetProductUseCase(IProductReadOnlyRepository repositoryProductRead, ICommentReadOnlyRepository repositoryCommentRead, IMapper mapper, IAzureStorageService storageService, IUserReadOnlyRepository userReadOnly)
        {
            _repositoryProductRead = repositoryProductRead;
            _repositoryCommentRead = repositoryCommentRead;
            _mapper = mapper;
            _storageService = storageService;
            _repositoryUserRead = userReadOnly;
        }

        public async Task<ResponseProductFull> Execute(long id)
        {
            var product = await _repositoryProductRead.ProductById(id);

            var response = _mapper.Map<ResponseProductFull>(product);
            var commentResponse = product.Comments.Select(async comment =>
            {
                var response = _mapper.Map<ResponseComment>(comment);

                response.UserImage = await _storageService.GetUrlImageUser(comment.User, comment.User.ImageIdentifier);

                return response;
            });

            var commentResult = await Task.WhenAll(commentResponse);

            response.Comments = commentResult;
            response.ImageUrl = await _storageService.GetUrlImageProduct(product, product.ImageIdentifier!);

            return response;
        }
    }
}
